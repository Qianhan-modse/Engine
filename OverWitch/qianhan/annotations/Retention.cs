using InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler;

namespace InfiniteMemories.OverWitch.qianhan.annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    [Retention(RetentionPolicy.RUNTIME)]
    [Target(ElementType.ANNOTATION_TYPE)]
    [Documented]
    public class Retention:Attribute
    {
        public RetentionPolicy Policy { get; }
        public Retention(RetentionPolicy policy)
        {
            Policy = policy;
        }
    }
}
