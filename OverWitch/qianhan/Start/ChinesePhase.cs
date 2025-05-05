namespace InfiniteMemories.OverWitch.qianhan.Start
{
    /// <summary>
    /// 枚举定义执行优先级，事件优先级最高
    /// </summary>
    [Flags]
    public enum ChinesePhase
    {
        //事件优先级最高
        Events = 1,//1<<0
        //用户的优先级同等于Events
        User = 1,//同Events
        //初期初始化，优先级仅次于Events
        Awake = 2,//1<<1
        //初始加载，优先级处于高阶层
        Start = 4,//1<<2
        //更新，优先级处于较高阶层
        Update = 8,//1<<3
        //更新后，优先级处于较低阶层
        LateUpdate = 16,//1<<4
        //物理更新，优先级处于较低阶层
        FixedUpdate = 32,//1<<5
        //当渲染启动时，位于高阶层，此为渲染层
        onRender = 64,//1<<6
        //当UI启动时，位于高阶层，此为UI层
        onGUI = 128,//1<<7
        //当物理启动时，位于高阶层，此为物理层
        onPhysics = 256,//1<<8
        //当网络启动时，位于高阶层，此为网络层
        onNetWorkSync = 512,//1<<9
        //当AI被启动时，位于高阶层，此为AI层
        AiTick = 1024,//1<<10
        //Tick时刻，位于高阶层，此为Tick层
        Tick = 2048,//1<<11
        //当Tick时刻被启动时，位于中等，此为Tick层
        MinTick = 4096,//1<<12
        //当Tick时刻被启动时，位于低等，此为Tick层
        CurrentTick = 8192,//1<<13
        //当Awake启动时，此可以被用于Awake的回调
        onAwake = 16384,//1<<14
        //当Start启动时，此可以被用于Start的回调
        onStart = 32768,//1<<15
        //当Update启动时，此可以被用于Update的回调
        onUpdate = 65536,//1<<16
        //当LateUpdate启动时，此可以被用于LateUpdate的回调
        onLateUpdate = 131072,//1<<17
        //当FixedUpdate启动时，此可以被用于FixedUpdate的回调
        onFixedUpdate = 262144,//1<<18
        //当Awake结束时，此可以被用于Awake的回调
        unAwake = 524288,//1<<19
        //当Start结束时，此可以被用于Start的回调
        unStart = 1048576,//1<<20
        //当Update结束时，此可以被用于Update的回调
        unUpdate = 2097152,//1<<21
        //当LateUpdate结束时，此可以被用于LateUpdate的回调
        unLateUpdate = 4194304,//1<<22
        //当FixedUpdate结束时，此可以被用于FixedUpdate的回调
        unFixedUpdate = 8388608,//1<<23
        //当Awake被移除时，可以用于生命周期的回溯或者回调
        removeAwake = 16777216,//1<<24
        //当Start被移除时，可以用于生命周期的回溯或者回调
        removeStart = 33554432,//1<<25
        //当Update被移除时，可以用于生命周期的回溯或者回调
        removeUpdate = 67108864,//1<<26
        //当LateUpdate被移除时，可以用于生命周期的回溯或者回调
        removeLateUpdate = 134217728,//1<<27
        //当FixedUpdate被移除时，可以用于生命周期的回溯或者回调
        removeFixedUpdate = 268435456,//1<<28
        //回调所有
        All = ~0//-1:所有为均为1
    }
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ChineseAttribute : Attribute
    {
        public ChinesePhase Phase;
        public ChineseAttribute(ChinesePhase phase)
        {
            this.Phase = phase;
        }
    }
}
