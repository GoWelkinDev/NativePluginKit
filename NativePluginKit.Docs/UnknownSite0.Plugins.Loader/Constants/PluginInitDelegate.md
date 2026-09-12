# PluginInitDelegate

```csharp
public delegate void PluginInitDelegate(IntPtr hostApiTable)
```

插件导出的初始化函数签名 插件必须导出一个名为 `OnInit` 的函数，参数为指向 HostApiTable 的指针

