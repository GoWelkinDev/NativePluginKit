using System.Runtime.InteropServices;
using UnknownSite0.Plugins.Loader;
using UnknownSite0.Plugins.Loader.Constants;

namespace ExampleClient
{
    public class ExampleClient
    {
        public static readonly PluginLoader PluginLoader = new PluginLoader(msg => Console.WriteLine(msg), HostApiBridge.CreateApiTable(), new Version(1, 0, 0));

        public static void Main(String[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            PluginLoader.LoadPluginsFromDirectory(Path.Combine(Environment.CurrentDirectory, "plugins"));
        }
    }

    public class HostApiBridge
    {
        // 构建 API 函数表
        public static HostApiTable CreateApiTable()
        {
            static void transition(nint pathPtr, bool fadeOut)
            {
                string? scenePath = Marshal.PtrToStringUTF8(pathPtr);
                if (string.IsNullOrEmpty(scenePath)) return;
            }

            static void print(nint msgPtr)
            {
                string? msg = Marshal.PtrToStringUTF8(msgPtr);
                if (!string.IsNullOrEmpty(msg)) Console.WriteLine(msg);
            }

            return HostApiBridgeBuilder.Create(transition, print);
        }
    }
}
