namespace InfiniteMemories.OverWitch.qianhan.MainColler
{
    /// <summary>
    /// 订阅生命周期注册表，请不要使用Subscribe
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SubscribeLifecycleRegistry : Attribute
    {
        public LifetcycleRegisterys Registery;
        public SubscribeLifecycleRegistry(LifetcycleRegisterys registery)
        {
            Registery = registery;
        }
    }
    /// <summary>
    /// 订阅生命周期枚举类
    /// </summary>
    [Flags]
    public enum LifetcycleRegisterys
    {
        /// <summary>
        /// 订阅Awake
        /// </summary>
        SubscribeAwake = 1,
        /// <summary>
        /// 订阅Start
        /// </summary>
        SubscribeStart = 16,
        /// <summary>
        /// 订阅Update
        /// </summary>
        SubscribeUpdate = 18,
        /// <summary>
        /// 订阅LateUpdate
        /// </summary>
        SubscribeLateUpdate = 38,
        /// <summary>
        /// 订阅FixedUpdate
        /// </summary>
        SubscribeFixedUpdate = 62,
    }
}
