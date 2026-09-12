# LoadPluginsFromDirectory(string)

```csharp
public void LoadPluginsFromDirectory(string directoryPath)
```

加载指定目录下的所有插件 (*.dll)

| Parameter | Type | Description |
| --- | --- | --- |
| directoryPath | string | 插件目录路径 |

若目录不存在则静默返回。每个DLL的加载过程会通过 LogWriter 记录。

