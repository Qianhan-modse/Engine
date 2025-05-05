using InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler;

namespace InfiniteMemories.OverWitch.qianhan.annotations.meta
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [Documented]
    [Target(ElementType.ANNOTATION_TYPE)]
    public class TypeQualifierNickname : Attribute
    {
    }
}
