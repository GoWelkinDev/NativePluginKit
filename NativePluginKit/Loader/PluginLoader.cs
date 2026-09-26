using NativePluginKit.Loader.Constants;
using System.Runtime.InteropServices;

namespace NativePluginKit.Loader
{
    /// <summary>
    /// 负责加载和管理插件的核心类
    /// 使用非托管代码 (LoadLibrary) 动态加载插件并调用其导出函数
    /// </summary>
    public unsafe class PluginLoader
    {
        private readonly List<PluginHandle> _plugins = new();

        /// <summary>
        /// 主程序暴露给插件的 API 函数表
        /// </summary>
        public HostApiTable Api { get; }

        /// <summary>
        /// 主程序的日志输出委托，用于记录加载过程信息
        /// </summary>
        public Action<string> LogWriter { get; }

        /// <summary>
        /// 主程序的 API 版本，用于与插件所需版本比较
        /// </summary>
        public Version CurrentApiVersion { get; }

        /// <summary>
        /// 初始化 <see cref="PluginLoader"/> 类的新实例
        /// </summary>
        /// <param name="logAction">用于接收日志消息的委托</param>
        /// <param name="apiTable">要传递给插件的主程序 API 函数表</param>
        /// <param name="apiVersion">主程序的 API 版本</param>
        public PluginLoader(Action<string> logAction, HostApiTable apiTable, Version apiVersion)
        {
            LogWriter = logAction;
            Api = apiTable;
            CurrentApiVersion = apiVersion;
        }

        /// <summary>
        /// 加载指定路径的插件，并调用其导出的 <c>OnInit</c> 函数
        /// </summary>
        /// <param name="dllPath">插件的完整路径</param>
        /// <returns>如果加载并初始化成功，返回 <c>true</c>; 否则返回 <c>false</c></returns>
        /// <remarks>
        /// 加载成功后模块句柄会保留，以便插件在宿主进程生命周期内保持加载状态
        /// 初始化函数指针类型为 <c>delegate* unmanaged&lt;IntPtr, void&gt;</c>
        /// </remarks>
        public bool LoadPlugin(string dllPath)
        {
            if (!NativeLibrary.TryLoad(dllPath, out IntPtr hModule))
            {
                LogWriter($"Failed to load {dllPath}");
                return false;
            }

            // 获取 PluginInfo
            if (!NativeLibrary.TryGetExport(hModule, "GetPluginInfo", out IntPtr infoFuncPtr))
            {
                LogWriter($"GetPluginInfo not found in {Path.GetFileName(dllPath)}. Plugin rejected.");
                NativeLibrary.Free(hModule);
                return false;
            }

            var infoFunc = (delegate* unmanaged<IntPtr>)infoFuncPtr;
            IntPtr pluginInfoPtr = infoFunc();
            if (pluginInfoPtr == IntPtr.Zero)
            {
                LogWriter($"GetPluginInfo returned null in {Path.GetFileName(dllPath)}. Plugin rejected.");
                NativeLibrary.Free(hModule);
                return false;
            }

            PluginInfo info = Marshal.PtrToStructure<PluginInfo>(pluginInfoPtr);

            // 版本兼容性检查
            Version requiredApi = info.GetRequiredApiVersion();
            if (requiredApi.Major != CurrentApiVersion.Major || requiredApi.Minor > CurrentApiVersion.Minor)
            {
                LogWriter($"Plugin '{info.GetName()}' requires API v{requiredApi}, but host provides v{CurrentApiVersion}. Plugin rejected.");
                NativeLibrary.Free(hModule);
                return false;
            }

            // 获取 OnInit
            if (!NativeLibrary.TryGetExport(hModule, "OnInit", out IntPtr initPtr))
            {
                LogWriter("OnInit not found");
                NativeLibrary.Free(hModule);
                return false;
            }

            LogWriter($"Loading plugin: {info.GetName()} v{info.GetVersion()} by {info.GetAuthor()}");

            var initFunc = (delegate* unmanaged<IntPtr, void>)initPtr;
            // 将结构体指针传递给插件
            IntPtr apiPtr = Marshal.AllocHGlobal(Marshal.SizeOf<HostApiTable>());
            Marshal.StructureToPtr(Api, apiPtr, false);

            try
            {
                initFunc(apiPtr);
            }
            catch (Exception ex)
            {
                LogWriter($"Exception during OnInit of '{info.GetName()}': {ex.Message}");
                Marshal.FreeHGlobal(apiPtr);
                NativeLibrary.Free(hModule);
                return false;
            }
            finally
            {
                // apiPtr 在插件内部已复制，可安全释放
                Marshal.FreeHGlobal(apiPtr);
            }

            _plugins.Add(new PluginHandle
            {
                DllPath = dllPath,
                ModuleHandle = hModule,
                Info = info
            });

            return true;
        }

        /// <summary>
        /// 加载指定目录下的所有插件 (*.dll)
        /// </summary>
        /// <param name="directoryPath">插件目录路径</param>
        /// <remarks>
        /// 若目录不存在则静默返回。每个DLL的加载过程会通过 <see cref="LogWriter"/> 记录。
        /// </remarks>
        public void LoadPluginsFromDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                return;
            }

            string pattern = GetPlatformPattern();

            string[] dllFiles = Directory.GetFiles(directoryPath, pattern);

            foreach (string dllPath in dllFiles)
            {
                LoadPlugin(dllPath);
            }
        }

        /// <summary>
        /// 卸载所有已加载的插件并释放库句柄
        /// </summary>
        public void UnloadAll()
        {
            foreach (var handle in _plugins)
            {
                LogWriter($"Unloading plugin: {handle.Info.GetName()}");

                if (NativeLibrary.TryGetExport(handle.ModuleHandle, "OnStop", out IntPtr stopPtr))
                {
                    var stopFunc = (delegate* unmanaged<void>)stopPtr;
                    try
                    {
                        stopFunc();
                    }
                    catch (Exception ex)
                    {
                        LogWriter($"Exception during OnStop of '{handle.Info.GetName()}': {ex.Message}");
                    }
                }

                NativeLibrary.Free(handle.ModuleHandle);
                LogWriter($"Unloaded plugin: {handle.Info.GetName()}");
            }

            _plugins.Clear();
        }

        private string GetPlatformPattern()
        {
            if (OperatingSystem.IsWindows()) return "*.dll";
            if (OperatingSystem.IsMacOS()) return "*.dylib";
            return "*.so";
        }
    }

    internal class PluginHandle
    {
        public string DllPath = string.Empty;
        public IntPtr ModuleHandle;
        public PluginInfo Info;
    }
}
