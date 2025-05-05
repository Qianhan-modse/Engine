using InfiniteMemories.OverWitch.qianhan.annotations;

namespace InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public class Target:Attribute
    {
        public ElementType[] Targets { get; }
        public Target(params ElementType[] types)
        {
            Targets = types;
        }
    }
}
