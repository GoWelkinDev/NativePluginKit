# OnInit(IntPtr)

```csharp
public static void OnInit(IntPtr hostApiTablePtr)
```

插件导出的初始化入口点，由插件加载器通过 `OnInit` 名称调用

| Parameter | Type | Description |
| --- | --- | --- |
| hostApiTablePtr | IntPtr | 指向宿主 API 函数表的非托管指针 |

此方法被 UnmanagedCallersOnlyAttribute 标记，
表示仅可由非托管代码调用。方法内部创建插件实例并调用
Plugin.Initialize() 完成初始化。

