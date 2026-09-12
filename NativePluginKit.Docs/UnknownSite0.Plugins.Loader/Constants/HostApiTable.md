# HostApiTable

```csharp
public struct HostApiTable
```

主程序暴露给插件的函数指针表，包含插件可调用的宿主功能 结构体采用顺序布局，与非托管内存直接对应

## Fields

| Member | Type | Description |
| --- | --- | --- |
| [PrintLog](HostApiTable/PrintLog.md) | IntPtr | 指向 `PrintLog` 函数的指针 函数签名：`void PrintLog(IntPtr messagePtr)` |
| [TransitionToScene](HostApiTable/TransitionToScene.md) | IntPtr | 指向 `TransitionToScene` 函数的指针 函数签名：`void TransitionToScene(IntPtr scenePathPtr, bool fadeOut)` |

