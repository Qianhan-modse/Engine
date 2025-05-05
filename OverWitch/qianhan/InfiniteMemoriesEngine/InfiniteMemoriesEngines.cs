using InfiniteMemories.OverWitch.qianhan.Attributes;
using InfiniteMemories.OverWitch.qianhan.MainColler.InfiniteMemories.InfiniteMemoriesEngine.All;
using InfiniteMemories.OverWitch.qianhan.Objects;
using InfiniteMemories.OverWitch.qianhan.Start;
using OverWitch.qianhan.Util;

namespace InfiniteMemories.OverWitch.qianhan.InfiniteMemoriesEngine
{
    public class InfiniteMemoriesEngines
    {
        //首先声明一个对象，虽然可能是无意义的，但，他的确有效；
        public static object ess;
        //主要布尔值，控制引擎是否能够跑动
        private static bool MainBool;
        //主要时刻，控制引擎加载频率
        public int Maintick = 0;
        private bool isBool;
        private static MainObject obj;
        private static ChinesePhase ChinesePhase = new ChinesePhase();
        DateTime lastMemoryTrimTime = DateTime.Now;
        TimeSpan trimInterval = TimeSpan.FromMinutes(5);

        public InfiniteMemoriesEngines()
        {
            MainBool = true;
            LifecycleInvoker lifecycleInvoker = new();
            var context = new DefaultLifecycleContextProvider();
        }
        public static void Main(string[]args)
        {
            DllRegistrar.RegisterAll();
            obj =new MainObject();
            MainBool = true;
            var context = new DefaultLifecycleContextProvider();
            InfiniteMemoriesEngines engines = new InfiniteMemoriesEngines();
            LifecycleInvoker.SetContextProvider(context);
            LifecycleInvoker.InvokeByPhase(ess,ChinesePhase);
            Console.WriteLine(Environment.Is64BitProcess ? "当前为 64 位进程" : "当前为 32 位进程");

            while (true)
            {
                Thread.Sleep(550);
                engines.MainUpdate();
                LifecycleDispatcher.Update();
                LifecycleDispatcher.onUpdate();
            }
        }
        /// <summary>
        /// 通过枚举修饰在更新启动时调用而不是更新时调用
        /// </summary>
        [Chinese(ChinesePhase.onUpdate)]
        public virtual void MainUpdate()
        {
            if (MainBool)
            {
                Console.WriteLine("e");
                Tick();
            }
        }
        private void Tick()
        {
            if(DateTime.Now-lastMemoryTrimTime>trimInterval)
            {
                MainObject.removeObjects();
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);
                GC.WaitForPendingFinalizers();
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);
                MainObject.remove();
                lastMemoryTrimTime = DateTime.Now;
            }
        }
    }
}
