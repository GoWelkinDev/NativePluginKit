# HostApiBridgeBuilder

```csharp
public static class HostApiBridgeBuilder
```

用于构建 HostApiTable 的静态工厂类 将托管委托转换为非托管函数指针

## Methods

| Member | Returns | Description |
| --- | --- | --- |
| [Create(TransitionToSceneDelegate, PrintLogDelegate)](HostApiBridgeBuilder/Create.md) | [HostApiTable](HostApiTable.md) | 创建填充了函数指针的 HostApiTable 实例 |

## Nested Types

| Type | Description |
| --- | --- |
| [PrintLogDelegate](HostApiBridgeBuilder/PrintLogDelegate.md) | 表示日志输出函数的委托，与宿主API中的 `PrintLog` 匹配 |
| [TransitionToSceneDelegate](HostApiBridgeBuilder/TransitionToSceneDelegate.md) | 表示场景切换函数的委托，与宿主API中的 `TransitionToScene` 匹配 |

