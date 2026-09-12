# TransitionToScene(string, bool)

```csharp
public static void TransitionToScene(string scenePath, bool fadeOut)
```

请求宿主程序切换到指定场景

| Parameter | Type | Description |
| --- | --- | --- |
| scenePath | string | 场景路径 (UTF-8) |
| fadeOut | bool | 是否使用淡出过渡效果，默认为 `true` |

场景路径以非托管UTF-8字符串传递，调用后立即释放内存

