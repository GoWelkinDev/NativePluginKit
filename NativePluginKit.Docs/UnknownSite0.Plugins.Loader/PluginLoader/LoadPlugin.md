# LoadPlugin(string)

```csharp
public bool LoadPlugin(string dllPath)
```

加载指定路径的插件，并调用其导出的 `OnInit` 函数

| Parameter | Type | Description |
| --- | --- | --- |
| dllPath | string | 插件的完整路径 |

**Returns:** 如果加载并初始化成功，返回 `true`; 否则返回 `false`

加载成功后模块句柄会保留，以便插件在宿主进程生命周期内保持加载状态
初始化函数指针类型为 `delegate* unmanaged<IntPtr, void>`

