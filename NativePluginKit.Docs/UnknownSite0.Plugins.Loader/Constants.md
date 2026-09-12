# UnknownSite0.Plugins.Loader.Constants

| Type | Description |
| --- | --- |
| [HostApiBridgeBuilder](Constants/HostApiBridgeBuilder.md) | 用于构建 HostApiTable 的静态工厂类 将托管委托转换为非托管函数指针 |
| [HostApiTable](Constants/HostApiTable.md) | 主程序暴露给插件的函数指针表，包含插件可调用的宿主功能 结构体采用顺序布局，与非托管内存直接对应 |
| [PluginInitDelegate](Constants/PluginInitDelegate.md) | 插件导出的初始化函数签名 插件必须导出一个名为 `OnInit` 的函数，参数为指向 HostApiTable 的指针 |

