using System.Reflection;
using System.Runtime.InteropServices;

namespace InfiniteMemories.OverWitch.qianhan.Attributes
{
    public static class DllRegistrar
    {
        public static void RegisterAll()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in assemblies)
            {
                foreach (var type in asm.GetTypes())
                {
                    foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        var attr = method.GetCustomAttribute<import>();
                        if (attr != null)
                        {
                            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, attr.DllName);
                            if (File.Exists(fullPath))
                            {
                                DllLoader.Load(fullPath);
                            }
                            else
                            {
                                Console.WriteLine($"[Warning] DLL 未找到: {fullPath}");
                            }
                        }
                    }
                }
            }
        }
    }
    public static class DllLoader
    {
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        public static IntPtr Load(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"[DllLoader] 文件不存在: {path}");
                return IntPtr.Zero;
            }

            var ptr = LoadLibrary(path);
            if (ptr == IntPtr.Zero)
            {
                Console.WriteLine($"[DllLoader] 加载失败: {path}");
            }
            else
            {
                Console.WriteLine($"[DllLoader] 成功加载: {path}");
            }
            return ptr;
        }
    }
}
