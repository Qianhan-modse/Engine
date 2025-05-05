namespace InfiniteMemories.OverWitch.qianhan.Enums
{
    /// <summary>
    /// 用于标记对象的销毁状态或者是否将其彻底移除，通过枚举控制可以节省不必要的GC开销。
    /// </summary>
    [Flags]
    public enum Destory
    {
        /// <summary>
        /// 当没有什么标记的情况下可以用它视为无标记
        /// </summary>
        None = 0,
        /// <summary>
        /// 移除标记为不再使用的对象
        /// </summary>
        remove = 1,
        /// <summary>
        /// 移除标记为销毁的对象
        /// </summary>
        removeDestory = 2,
        /// <summary>
        /// 移除标记为销毁的全体对象和不再使用的全体对象
        /// </summary>
        removeDestoryAll = 4,
        /// <summary>
        /// 销毁标记为销毁的对象
        /// </summary>
        Destory = 8,
        /// <summary>
        /// 销毁标记为销毁的全体对象和不再使用的全体对象
        /// </summary>
        DestoryAll = 16,
        /// <summary>
        /// 不销毁标记为不销毁的对象
        /// </summary>
        NoDestory = 32,
        /// <summary>
        /// 不销毁标记为不销毁的全体对象和不再使用的全体对象
        /// </summary>
        NoDestoryAll = 64,
        /// <summary>
        /// 如果不再使用，请使用此标记
        /// </summary>
        NoLongerUsed = 128,
        /// <summary>
        /// 如果不再使用，请使用此标记
        /// </summary>
        NoLongerUsedAll = 256,
        /// <summary>
        /// 取消调用
        /// </summary>
        CancelCall = 512,
        /// <summary>
        /// 取消全部调用
        /// </summary>
        CancelCallAll = 1024,
        /// <summary>
        /// 取消销毁
        /// </summary>
        CancelDestory = 2048,
        /// <summary>
        /// 取消全部销毁
        /// </summary>
        CancelDestoryAll = 4096,
        /// <summary>
        /// 将指定方法标记为事件，请注意，如果是类必须继承Event
        /// </summary>
        setEvent = 8192,
        /// <summary>
        /// 将全部方法标记为事件，请注意，如果是类必须继承Event
        /// </summary>
        setEventAll = 16384,
        /// <summary>
        /// 返回标记为返回或不需要执行的对象
        /// </summary>
        Return = 32768,
        /// <summary>
        /// 返回标记为返回或不需要执行的全体对象
        /// </summary>
        ReturnAll = 65536,
        /// <summary>
        /// 手动调用标记为手动调用的对象
        /// </summary>
        ManuallyCall = 131072,
        /// <summary>
        /// 手动调用标记为手动调用的全体对象
        /// </summary>
        ManualCallAll = 262144,
        /// <summary>
        /// 不再调用标记为不再调用的对象
        /// </summary>
        DoNotCallAgain = 524288,
        /// <summary>
        /// 不再调用标记为不再调用的全体对象
        /// </summary>
        DoNotCallAgainAll = 1048576,
        /// <summary>
        /// 标记为结束的对象
        /// </summary>
        Over = 2097152,
        /// <summary>
        /// 标记为结束的全体对象
        /// </summary>
        OverAll = 4194304,
        /// <summary>
        /// 标记为所有的对象
        /// </summary>
        All = ~0
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class DestoryEnum : Attribute
    {
        private readonly Destory destory;

        /// <summary>
        /// 通过枚举控制可以节省不必要的GC开销
        /// </summary>
        /// <param name="destory"></param>
        public DestoryEnum(Destory destory)
        {
            this.destory = destory;
        }
    }
}
