using System.Diagnostics;
using System.Runtime.InteropServices;
using InfiniteMemories.OverWitch.qianhan.Attributes;

namespace InfiniteMemories.OverWitch.qianhan.Objects
{
    /// <summary>
    /// MainObject类，基础类，除了World、Item和Entity以及部分基础类外或多或少都需要继承它
    /// </summary>
    public class MainObject
    {

        //private const string DllName = @"I:/OverWitch/Source/OverWitch/qianhan/dll/ObjectManagerDLL.dll";
        //private const string DllName = @"OverWitch/qianhan/dll/ObjectManagerDLL.dll";
        //string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"OverWitch\qianhan\dll\ObjectManagerDLL.dll");

        [DllImport("ObjectManagerDLL.dll")]
        public static extern void registerType(string typeName, IntPtr destructor);

        [DllImport("ObjectManagerDLL.dll")]
        public static extern IntPtr createObject(string typeName, ulong size);

        [DllImport("ObjectManagerDLL.dll")]
        public static extern void removeObject(IntPtr ptr);

        [DllImport("ObjectManagerDLL.dll")]
        public static extern void removeObjectAll(long id);

        [DllImport("ObjectManagerDLL.dll")]
        public static extern void removeObjects();

        [DllImport("ObjectManagerDLL.dll")]
        public static extern void removeObjectAlls();

        [DllImport("ObjectManagerDLL.dll")]
        public static extern void removeObjectDestory(long id);

        [DllImport("ObjectManagerDLL.dll")]
        public static extern void removeObjectDestroy(string name);

        [DllImport("ObjectManagerDLL.dll")]
        public static extern void removeObjectsAll(string name);
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr hProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        /// <summary>
        /// 如果上面的移除无效的情况下，就使用这个方法
        /// </summary>
        /// <param name="object"></param>
        public static void remove()
        {
            try
            {
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);
                GC.WaitForPendingFinalizers();
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);

                using (Process p = Process.GetCurrentProcess())
                {
                    SetProcessWorkingSetSize(p.Handle, -1, -1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Aggressive memory trim failed: " + ex.Message);
            }
        }
    }
}
