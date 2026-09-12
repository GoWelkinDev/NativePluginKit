# Create(TransitionToSceneDelegate, PrintLogDelegate)

```csharp
public static HostApiTable Create(TransitionToSceneDelegate transition, PrintLogDelegate printLog)
```

创建填充了函数指针的 HostApiTable 实例

| Parameter | Type | Description |
| --- | --- | --- |
| transition | [TransitionToSceneDelegate](../../../TransitionToSceneDelegate.md) | 实现场景切换的托管委托 |
| printLog | [PrintLogDelegate](../../../PrintLogDelegate.md) | 实现日志输出的托管委托 |

**Returns:** 包含非托管函数指针的API表结构体

