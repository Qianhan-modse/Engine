using System.Collections;
using InfiniteMemories.OverWitch.qianhan.Objects;

namespace InfiniteMemories.OverWitch.qianhan.Enumerator
{
    /// <summary>
    /// 还未完全实现，只是一个猜想
    /// </summary>
    public class MainEnumerator : IEnumerator<MainEnumerator>
    {
        private int CurrentTick;
        private int MaxTick;
        private bool isDisposed;
        private int MinTick;
        /// <summary>
        /// 可根据需求使用的构造函数
        /// </summary>
        /// <param name="currentTick"></param>
        /// <param name="maxTick"></param>
        /// <param name="isDisposed"></param>
        /// <param name="minTick"></param>
        protected MainEnumerator(int currentTick, int maxTick, bool isDisposed, int minTick)
        {
            CurrentTick = currentTick;
            MaxTick = maxTick;
            this.isDisposed = isDisposed;
            MinTick = minTick;
        }
        /// <summary>
        /// 可被继承的构造函数无参数
        /// </summary>
        public MainEnumerator()
        {
            CurrentTick = 0;
            MaxTick = 30000;
            isDisposed = false;
            MinTick = 0;
        }
        public MainEnumerator Current => this;
        object IEnumerator.Current =>this.Current;
        /// <summary>
        /// 同携程，移动到下一个时刻
        /// </summary>
        /// <returns></returns>
        public bool MoveNext() 
        {
            if (CurrentTick>=MaxTick)
            {
                return false;
            }
            CurrentTick++;
            return true;
        }
        /// <summary>
        /// 重置迭代器回到最开始的状态
        /// </summary>
        public void Reset()
        {
            CurrentTick = 0;
        }
        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            //释放掉内存,主要的外置逻辑
            MainObject.removeObjectAlls();
            //GC代清理，兜底
            GC.Collect(0);
            GC.WaitForPendingFinalizers();
            //第一代
            GC.Collect(1);
            GC.WaitForPendingFinalizers();
            //第二代
            GC.Collect(2);
            GC.WaitForPendingFinalizers();
            //第三代，虽然不可能
            GC.Collect(3);
            GC.WaitForPendingFinalizers();
            isDisposed = true;
        }
    }
}
