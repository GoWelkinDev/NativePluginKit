// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using NativePluginKit.Events;
using NativePluginKit.Features.Cards;
using NativePluginKit.Features.Configs;
using NativePluginKit.Features.Entities;
using NativePluginKit.Features.Events;
using NativePluginKit.Features.Logger;
using NativePluginKit.Features.Story;
using NativePluginKit.Features.Wrappers;
using NativePluginKit.Loader.Plugins;
using NativePluginKit.Loader.Plugins.Metadata;

namespace HelloWorldPlugin
{
    /// <summary>
    /// 覆盖 NativePluginKit 全部公开接口的示例插件
    /// </summary>
    public partial class HelloWorldPlugin : Plugin, IPluginMetadata
    {
        public static string Name => "HelloWorldPlugin";
        public static string Description => "Exercises every NativePluginKit API.";
        public static string Author => "GoWelkinDev";
        public static Version RequiredApiVersion => new(2, 0, 0);
        public static Version Version => new(1, 0, 0);

        protected override void OnStart()
        {
            Log.PrintLog("=== HelloWorldPlugin: begin ===");

            TestLogger();
            TestTiming();
            TestConfig();
            TestCards();
            TestEntities();
            TestStory();
            SubscribeAllEvents();

            Log.PrintLog("=== HelloWorldPlugin: end ===");
        }

        public override void OnStop()
        {
            UnsubscribeAllEvents();
            Log.PrintLog("HelloWorldPlugin stopped.");
        }

        private void TestLogger()
        {
            Log.PrintLog("[Log] PrintLog");
            Log.PrintWarning("[Log] PrintWarning");
            Log.PrintError("[Log] PrintError");
            Log.Debug("[Log] Debug");
        }

        private void TestTiming()
        {
            double t = Config.GetTimeSinceStartup();
            Log.PrintLog($"[Timing] GetTimeSinceStartup = {t:F3}s");
        }

        private void TestConfig()
        {
            string pluginDir = Config.GetPluginsDirectory();

            Log.PrintLog($"[Config] PluginsDirectory     = {pluginDir}");
        }

        private void TestCards()
        {
            int count = Card.GetCardCount();
            Log.PrintLog($"[Card] GetCardCount = {count}");

            for (int i = 0; i < count; i++)
            {
                string name = Card.GetCardNameAt(i);
                if (string.IsNullOrEmpty(name)) continue;
                if (!Card.Contains(name)) continue;

                Log.PrintLog(
                    $"[Card] - {name}" +
                    $" | {Card.GetDisplayName(name)}" +
                    $" | Cost={Card.GetCost(name)}" +
                    $" | ATK={Card.GetBaseAttack(name)}" +
                    $" | Stack={Card.GetMaxStack(name)}" +
                    $" | Weight={Card.GetWeight(name)}" +
                    $" | AttackType={Card.GetAttackTypeEnum(name)}" +
                    $" | Type={Card.GetCardTypeEnum(name)}");

                Log.PrintLog($"      Description: {Card.GetDescription(name)}");
                Log.PrintLog($"      ImageName:   {Card.GetImageName(name)}");

                CardEffectInfo[] effects = Card.GetAppliedEffects(name);
                for (int j = 0; j < effects.Length; j++)
                {
                    var e = effects[j];
                    Log.PrintLog(
                        $"[Effect] -     effect[{j}]: {e.EffectId} ({e.Name})" +
                        $" | {e.Category}" +
                        $" | Dur={e.Duration}" +
                        $" | Stackable={e.IsStackable}" +
                        $" | MaxStacks={e.MaxStacks}" +
                        $" | P1={e.Param1} P2={e.Param2}");
                }
            }
        }

        private void TestEntities()
        {
            int count = Entity.GetEntityCount();
            Log.PrintLog($"[Entity] GetEntityCount = {count}");

            foreach (EntityId id in Entity.GetAllEntityIds())
            {
                if (id.IsEmpty) continue;
                if (!Entity.Contains(id))
                {
                    Log.PrintLog($"[Entity] - {id} (gone)");
                    continue;
                }

                Log.PrintLog(
                    $"[Entity] - {Entity.GetDisplayName(id)}" +
                    $" | Name={Entity.GetName(id)}" +
                    $" | Image={Entity.GetImagePath(id)}" +
                    $" | HP={Entity.GetCurrentHP(id)}/{Entity.GetMaxHP(id)}" +
                    $" | ATK={Entity.GetAttackPower(id)}" +
                    $" | Dodge={Entity.GetDodgeChance(id)}" +
                    $" | Shield={Entity.GetShieldValue(id)}" +
                    $" | Armor={Entity.GetArmorValue(id)}" +
                    $" | Cost={Entity.GetCurrentCost(id)}/{Entity.GetMaxCost(id)}" +
                    $" | Alive={Entity.IsAlive(id)} Dead={Entity.IsDead(id)}" +
                    $" | CanAct={Entity.CanAct(id)}" +
                    $" | Player={Entity.IsPlayer(id)} Mob={Entity.IsMob(id)}");

                EntityEffectInfo[] effects = Entity.GetAllEffects(id);
                for (int j = 0; j < effects.Length; j++)
                {
                    var e = effects[j];
                    Log.PrintLog(
                        $"[Effect] -     effect[{j}]: {e.EffectId} ({e.Name})" +
                        $" | {e.Category}" +
                        $" | Remaining={e.RemainingDuration}" +
                        $" | Stacks={e.CurrentStacks}/{e.MaxStacks}");
                }
            }
        }

        private void TestStory()
        {
            string timeline = Story.GetCurrentTimelineName();
            bool inBattle = Story.IsBattle();
            Log.PrintLog($"[Story] Timeline = {timeline}");
            Log.PrintLog($"[Story] IsBattle = {inBattle}");
        }

        private void SubscribeAllEvents()
        {
            EventBus.On<SceneEvent>(EventType.SceneChanging, OnSceneChanging);
            EventBus.On<SceneEvent>(EventType.SceneChanged, OnSceneChanged);

            EventBus.On<TimelineEvent>(EventType.TimelineStarted, OnTimelineStarted);
            EventBus.On<TimelineEvent>(EventType.TimelineEnded, OnTimelineEnded);
            EventBus.On<TimelineSignalEvent>(EventType.TimelineSignal, OnTimelineSignal);

            EventBus.On<BattleEvent>(EventType.BattleStarted, OnBattleStarted);
            EventBus.On<BattleEvent>(EventType.BattleEnded, OnBattleEnded);

            EventBus.On<TurnEvent>(EventType.TurnStarted, OnTurnStarted);
            EventBus.On<TurnEvent>(EventType.TurnEnded, OnTurnEnded);

            EventBus.On<BattleMessageEvent>(EventType.BattleMessage, OnBattleMessage);

            EventBus.On<CardEvent>(EventType.CardDrawn, OnCardDrawn);
            EventBus.On<CardEvent>(EventType.CardPlayRequested, OnCardPlayRequested);
            EventBus.On<CardEvent>(EventType.CardPlayed, OnCardPlayed);
            EventBus.On<CardEvent>(EventType.CardDiscarded, OnCardDiscarded);

            EventBus.On<EntityDamagedEvent>(EventType.EntityDamaged, OnEntityDamaged);
            EventBus.On<EntityDiedEvent>(EventType.EntityDied, OnEntityDied);
            EventBus.On<EntityUpdatedEvent>(EventType.EntityUpdated, OnEntityUpdated);

            EventBus.On<EffectEvent>(EventType.EffectApplied, OnEffectApplied);
            EventBus.On<EffectEvent>(EventType.EffectExpired, OnEffectExpired);

            Log.PrintLog("[EventBus] subscribed to 19 events");
        }

        private void UnsubscribeAllEvents()
        {
            EventBus.Off<SceneEvent>(EventType.SceneChanging, OnSceneChanging);
            EventBus.Off<SceneEvent>(EventType.SceneChanged, OnSceneChanged);

            EventBus.Off<TimelineEvent>(EventType.TimelineStarted, OnTimelineStarted);
            EventBus.Off<TimelineEvent>(EventType.TimelineEnded, OnTimelineEnded);
            EventBus.Off<TimelineSignalEvent>(EventType.TimelineSignal, OnTimelineSignal);

            EventBus.Off<BattleEvent>(EventType.BattleStarted, OnBattleStarted);
            EventBus.Off<BattleEvent>(EventType.BattleEnded, OnBattleEnded);
            EventBus.Off<TurnEvent>(EventType.TurnStarted, OnTurnStarted);
            EventBus.Off<TurnEvent>(EventType.TurnEnded, OnTurnEnded);
            EventBus.Off<BattleMessageEvent>(EventType.BattleMessage, OnBattleMessage);

            EventBus.Off<CardEvent>(EventType.CardDrawn, OnCardDrawn);
            EventBus.Off<CardEvent>(EventType.CardPlayRequested, OnCardPlayRequested);
            EventBus.Off<CardEvent>(EventType.CardPlayed, OnCardPlayed);
            EventBus.Off<CardEvent>(EventType.CardDiscarded, OnCardDiscarded);

            EventBus.Off<EntityDamagedEvent>(EventType.EntityDamaged, OnEntityDamaged);
            EventBus.Off<EntityDiedEvent>(EventType.EntityDied, OnEntityDied);
            EventBus.Off<EntityUpdatedEvent>(EventType.EntityUpdated, OnEntityUpdated);

            EventBus.Off<EffectEvent>(EventType.EffectApplied, OnEffectApplied);
            EventBus.Off<EffectEvent>(EventType.EffectExpired, OnEffectExpired);
        }

        private void OnSceneChanging(SceneEvent e)
            => Log.PrintLog($"[Event] SceneChanging -> {e.ScenePath}");

        private void OnSceneChanged(SceneEvent e)
            => Log.PrintLog($"[Event] SceneChanged -> {e.ScenePath}");

        private void OnTimelineStarted(TimelineEvent e)
            => Log.PrintLog($"[Event] TimelineStarted -> {e.TimelineName} (battle={e.IsBattle})");

        private void OnTimelineEnded(TimelineEvent e)
            => Log.PrintLog($"[Event] TimelineEnded -> {e.TimelineName} (battle={e.IsBattle})");

        private void OnTimelineSignal(TimelineSignalEvent e)
            => Log.PrintLog($"[Event] TimelineSignal -> {e.SignalName}");

        private void OnBattleStarted(BattleEvent e)
            => Log.PrintLog($"[Event] BattleStarted");

        private void OnBattleEnded(BattleEvent e)
            => Log.PrintLog($"[Event] BattleEnded -> victory={e.Victory == 1}");

        private void OnTurnStarted(TurnEvent e)
            => Log.PrintLog($"[Event] TurnStarted -> turn {e.Turn}");

        private void OnTurnEnded(TurnEvent e)
            => Log.PrintLog($"[Event] TurnEnded -> turn {e.Turn}");

        private void OnBattleMessage(BattleMessageEvent e)
            => Log.PrintLog($"[Event] BattleMessage -> {e.Message}");

        private void OnCardDrawn(CardEvent e)
        {
            var owner = e.Owner;
            Log.PrintLog($"[Event] CardDrawn -> owner={owner} card={e.CardName}");
        }

        private void OnCardPlayRequested(CardEvent e)
        {
            var owner = e.Owner;
            var target = e.Target;
            Log.PrintLog($"[Event] CardPlayRequested -> owner={owner} card={e.CardName} target={target}");
        }

        private void OnCardPlayed(CardEvent e)
        {
            var owner = e.Owner;
            var target = e.Target;
            Log.PrintLog($"[Event] CardPlayed -> owner={owner} card={e.CardName} target={target}");
        }

        private void OnCardDiscarded(CardEvent e)
        {
            var owner = e.Owner;
            Log.PrintLog($"[Event] CardDiscarded -> owner={owner} card={e.CardName}");
        }

        private void OnEntityDamaged(EntityDamagedEvent e)
        {
            var id = e.Entity;
            Log.PrintLog(
                $"[Event] EntityDamaged -> {id}" +
                $" | raw={e.RawDamage} hp={e.HpDamage}" +
                $" | type={(CardAttackType)e.AttackType} remaining={e.RemainingHP}" +
                $" | dodged={e.Dodged}" +
                $" | shield={e.ShieldAbsorbed} armor={e.ArmorAbsorbed}");
        }

        private void OnEntityDied(EntityDiedEvent e)
        {
            var id = e.Entity;
            Log.PrintLog($"[Event] EntityDied -> {id}");
        }

        private void OnEntityUpdated(EntityUpdatedEvent e)
        {
            var id = e.Entity;
            Log.PrintLog($"[Event] EntityUpdated -> {id} HP={e.CurrentHP} Cost={e.CurrentCost}");
        }

        private void OnEffectApplied(EffectEvent e)
        {
            var owner = e.Owner;
            Log.PrintLog(
                $"[Event] EffectApplied -> owner={owner}" +
                $" effect={e.EffectId} ({e.EffectName})" +
                $" remaining={e.RemainingDuration} stacks={e.CurrentStacks}");
        }

        private void OnEffectExpired(EffectEvent e)
        {
            var owner = e.Owner;
            Log.PrintLog(
                $"[Event] EffectExpired -> owner={owner}" +
                $" effect={e.EffectId} ({e.EffectName})");
        }
    }
}