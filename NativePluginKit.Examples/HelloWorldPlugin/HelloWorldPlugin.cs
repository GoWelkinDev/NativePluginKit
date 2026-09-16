using System.Runtime.InteropServices;
using NativePluginKit.Events;
using NativePluginKit.Features.Cards;
using NativePluginKit.Features.Config;
using NativePluginKit.Features.Entities;
using NativePluginKit.Features.Events;
using NativePluginKit.Features.Logger;
using NativePluginKit.Features.Scene;
using NativePluginKit.Features.Story;
using NativePluginKit.Features.Timing;
using NativePluginKit.Features.Wrappers;
using NativePluginKit.Loader.Plugins;

namespace HelloWorldPlugin
{
    /// <summary>
    /// 覆盖 NativePluginKit 全部公开接口的示例插件。
    /// 加载后会在宿主日志中输出所有 API 的调用结果，并订阅全部事件类型。
    /// </summary>
    public partial class HelloWorldPlugin : Plugin
    {
        private static HelloWorldPlugin? _self;

        public override string Name => "HelloWorldPlugin";
        public override string Description => "Exercises every NativePluginKit API.";
        public override string Author => "GoWelkinDev";
        public override Version RequiredApiVersion => new(1, 0, 0);
        public override Version Version => new(1, 0, 0);

        // ==================================================================
        // 生命周期
        // ==================================================================

        protected override void OnStart()
        {
            _self = this;
            Log.PrintLog("=== HelloWorldPlugin: begin ===");

            TestLogger();
            TestTiming();
            TestConfig();
            TestScene();
            TestCards();
            TestEntities();
            TestStory();
            SubscribeAllEvents();

            Log.PrintLog("=== HelloWorldPlugin: end ===");
        }

        public override void OnStop()
        {
            UnsubscribeAllEvents();
            _self = null;
            Log.PrintLog("HelloWorldPlugin stopped.");
        }

        // ==================================================================
        // Logger
        // ==================================================================

        private static void TestLogger()
        {
            Log.PrintLog("[Log] PrintLog");
            Log.PrintWarning("[Log] PrintWarning");
            Log.PrintError("[Log] PrintError");
            Log.Debug("[Log] Debug");
        }

        // ==================================================================
        // Timing
        // ==================================================================

        private static void TestTiming()
        {
            double t = Timing.GetTimeSinceStartup();
            Log.PrintLog($"[Timing] GetTimeSinceStartup = {t:F3}s");
        }

        // ==================================================================
        // Config
        // ==================================================================

        private static void TestConfig()
        {
            string pluginDir = Config.GetPluginsDirectory();
            string imagePath = Config.GetCharacterImagePath("Jiang_Qiuyue");

            Log.PrintLog($"[Config] PluginsDirectory     = {pluginDir}");
            Log.PrintLog($"[Config] CharacterImagePath   = {imagePath}");
        }

        // ==================================================================
        // Scene
        // ==================================================================

        private static void TestScene()
        {
            string scene = Scene.GetCurrentScenePath();
            Log.PrintLog($"[Scene] CurrentScenePath = {scene}");
        }

        // ==================================================================
        // Cards
        // ==================================================================

        private static void TestCards()
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

        // ==================================================================
        // Entities
        // ==================================================================

        private static void TestEntities()
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

        // ==================================================================
        // Story
        // ==================================================================

        private static void TestStory()
        {
            string timeline = Story.GetCurrentTimelineName();
            bool inBattle = Story.IsBattle();
            Log.PrintLog($"[Story] Timeline = {timeline}");
            Log.PrintLog($"[Story] IsBattle = {inBattle}");
        }

        // ==================================================================
        // 事件订阅：覆盖全部 21 种事件
        // ==================================================================

        private static void SubscribeAllEvents()
        {
            // 1xx — 插件生命周期（不订阅自身）
            // 2xx — 场景
            EventBus.On<SceneEvent>(EventType.SceneChanging, OnSceneChanging);
            EventBus.On<SceneEvent>(EventType.SceneChanged, OnSceneChanged);

            // 3xx — 剧情
            EventBus.On<TimelineEvent>(EventType.TimelineStarted, OnTimelineStarted);
            EventBus.On<TimelineEvent>(EventType.TimelineEnded, OnTimelineEnded);
            EventBus.On<TimelineSignalEvent>(EventType.TimelineSignal, OnTimelineSignal);

            // 4xx — 战斗
            EventBus.On<BattleEvent>(EventType.BattleStarted, OnBattleStarted);
            EventBus.On<BattleEvent>(EventType.BattleEnded, OnBattleEnded);
            EventBus.On<TurnEvent>(EventType.TurnStarted, OnTurnStarted);
            EventBus.On<TurnEvent>(EventType.TurnEnded, OnTurnEnded);
            EventBus.On<BattleMessageEvent>(EventType.BattleMessage, OnBattleMessage);

            // 5xx — 卡牌
            EventBus.On<CardEvent>(EventType.CardDrawn, OnCardDrawn);
            EventBus.On<CardEvent>(EventType.CardPlayRequested, OnCardPlayRequested);
            EventBus.On<CardEvent>(EventType.CardPlayed, OnCardPlayed);
            EventBus.On<CardEvent>(EventType.CardDiscarded, OnCardDiscarded);

            // 6xx — 实体
            EventBus.On<EntityDamagedEvent>(EventType.EntityDamaged, OnEntityDamaged);
            EventBus.On<EntityDiedEvent>(EventType.EntityDied, OnEntityDied);
            EventBus.On<EntityUpdatedEvent>(EventType.EntityUpdated, OnEntityUpdated);

            // 7xx — 效果
            EventBus.On<EffectEvent>(EventType.EffectApplied, OnEffectApplied);
            EventBus.On<EffectEvent>(EventType.EffectExpired, OnEffectExpired);

            Log.PrintLog("[EventBus] subscribed to 19 events");
        }

        private static void UnsubscribeAllEvents()
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

        // ==================================================================
        // 事件回调
        // ==================================================================

        // ----- 场景 -----
        private static void OnSceneChanging(SceneEvent e)
            => Log.PrintLog($"[Event] SceneChanging → {Ptr(e.ScenePathPtr)}");

        private static void OnSceneChanged(SceneEvent e)
            => Log.PrintLog($"[Event] SceneChanged → {Ptr(e.ScenePathPtr)}");

        // ----- 剧情 -----
        private static void OnTimelineStarted(TimelineEvent e)
            => Log.PrintLog($"[Event] TimelineStarted → {Ptr(e.TimelineNamePtr)} (battle={e.IsBattle})");

        private static void OnTimelineEnded(TimelineEvent e)
            => Log.PrintLog($"[Event] TimelineEnded → {Ptr(e.TimelineNamePtr)} (battle={e.IsBattle})");

        private static void OnTimelineSignal(TimelineSignalEvent e)
            => Log.PrintLog($"[Event] TimelineSignal → {Ptr(e.SignalNamePtr)}");

        // ----- 战斗 -----
        private static void OnBattleStarted(BattleEvent e)
            => Log.PrintLog($"[Event] BattleStarted");

        private static void OnBattleEnded(BattleEvent e)
            => Log.PrintLog($"[Event] BattleEnded → victory={e.Victory == 1}");

        private static void OnTurnStarted(TurnEvent e)
            => Log.PrintLog($"[Event] TurnStarted → turn {e.Turn}");

        private static void OnTurnEnded(TurnEvent e)
            => Log.PrintLog($"[Event] TurnEnded → turn {e.Turn}");

        private static void OnBattleMessage(BattleMessageEvent e)
            => Log.PrintLog($"[Event] BattleMessage → {Ptr(e.MessagePtr)}");

        // ----- 卡牌 -----
        private static void OnCardDrawn(CardEvent e)
        {
            var owner = ToEntityId(e.OwnerEntityId);
            Log.PrintLog($"[Event] CardDrawn → owner={owner} card={Ptr(e.CardNamePtr)}");
        }

        private static void OnCardPlayRequested(CardEvent e)
        {
            var owner = ToEntityId(e.OwnerEntityId);
            var target = ToEntityId(e.TargetEntityId);
            Log.PrintLog($"[Event] CardPlayRequested → owner={owner} card={Ptr(e.CardNamePtr)} target={target}");
        }

        private static void OnCardPlayed(CardEvent e)
        {
            var owner = ToEntityId(e.OwnerEntityId);
            var target = ToEntityId(e.TargetEntityId);
            Log.PrintLog($"[Event] CardPlayed → owner={owner} card={Ptr(e.CardNamePtr)} target={target}");
        }

        private static void OnCardDiscarded(CardEvent e)
        {
            var owner = ToEntityId(e.OwnerEntityId);
            Log.PrintLog($"[Event] CardDiscarded → owner={owner} card={Ptr(e.CardNamePtr)}");
        }

        // ----- 实体 -----
        private static void OnEntityDamaged(EntityDamagedEvent e)
        {
            var id = ToEntityId(e.EntityId);
            Log.PrintLog(
                $"[Event] EntityDamaged → {id}" +
                $" | raw={e.RawDamage} hp={e.HpDamage}" +
                $" | type={(CardAttackType)e.AttackType} remaining={e.RemainingHP}" +
                $" | dodged={e.Dodged}" +
                $" | shield={e.ShieldAbsorbed} armor={e.ArmorAbsorbed}");
        }

        private static void OnEntityDied(EntityDiedEvent e)
        {
            var id = ToEntityId(e.EntityId);
            Log.PrintLog($"[Event] EntityDied → {id}");
        }

        private static void OnEntityUpdated(EntityUpdatedEvent e)
        {
            var id = ToEntityId(e.EntityId);
            Log.PrintLog($"[Event] EntityUpdated → {id} HP={e.CurrentHP} Cost={e.CurrentCost}");
        }

        // ----- 效果 -----
        private static void OnEffectApplied(EffectEvent e)
        {
            var owner = ToEntityId(e.OwnerEntityId);
            Log.PrintLog(
                $"[Event] EffectApplied → owner={owner}" +
                $" effect={Ptr(e.EffectIdPtr)} ({Ptr(e.EffectNamePtr)})" +
                $" remaining={e.RemainingDuration} stacks={e.CurrentStacks}");
        }

        private static void OnEffectExpired(EffectEvent e)
        {
            var owner = ToEntityId(e.OwnerEntityId);
            Log.PrintLog(
                $"[Event] EffectExpired → owner={owner}" +
                $" effect={Ptr(e.EffectIdPtr)} ({Ptr(e.EffectNamePtr)})");
        }

        // ==================================================================
        // 辅助
        // ==================================================================

        private static EntityId ToEntityId(ObjectIdNative n)
            => new EntityId(n.Low, n.High);

        private static string Ptr(IntPtr p)
        {
            if (p == IntPtr.Zero) return string.Empty;
            return Marshal.PtrToStringUTF8(p) ?? string.Empty;
        }
    }
}