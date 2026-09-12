# PluginLoader

```csharp
public class PluginLoader
```

负责加载和管理插件的核心类 使用非托管代码 (LoadLibrary) 动态加载插件并调用其导出函数

## Constructors

| Member | Description |
| --- | --- |
| [PluginLoader(Action<string>, Constants.HostApiTable)](PluginLoader/PluginLoader.md) | 初始化 PluginLoader 类的新实例 |

## Properties

| Member | Type | Description |
| --- | --- | --- |
| [Api](PluginLoader/Api.md) | [HostApiTable](Constants/HostApiTable.md) | 主程序暴露给插件的 API 函数表 |
| [LogWriter](PluginLoader/LogWriter.md) | Action\<string\> | 主程序的日志输出委托，用于记录加载过程信息 |

## Methods

| Member | Returns | Description |
| --- | --- | --- |
| [LoadPlugin(string)](PluginLoader/LoadPlugin.md) | bool | 加载指定路径的插件，并调用其导出的 `OnInit` 函数 |
| [LoadPluginsFromDirectory(string)](PluginLoader/LoadPluginsFromDirectory.md) | void | 加载指定目录下的所有插件 (*.dll) |

