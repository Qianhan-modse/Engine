using InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler;

namespace InfiniteMemories.OverWitch.qianhan.annotations.meta
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    [Documented]
    [Target(ElementType.ANNOTATION_TYPE)]
    [Retention(RetentionPolicy.RUNTIME)]
    public class TypeQualifier : Attribute
    {
        public Type ApplicableTo { get; }
        public TypeQualifier(Type applicableTo = null)
        {
            ApplicableTo = applicableTo ?? typeof(object);
        }
    }
}
