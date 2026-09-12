# Plugin

```csharp
public abstract class Plugin
```

所有插件的基类，提供与宿主程序交互的基础功能 插件继承此类并实现 Plugin.OnStart 方法

## Properties

| Member | Type | Description |
| --- | --- | --- |
| [Api](Plugin/Api.md) | [HostApiTable](../Constants/HostApiTable.md) | 获取宿主程序提供的 API 函数表，插件可通过它调用宿主功能 |

