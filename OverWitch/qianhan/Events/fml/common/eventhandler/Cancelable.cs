using InfiniteMemories.OverWitch.qianhan.annotations;

namespace InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
    public class Cancelable:Attribute
    {

    }
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    [Retention(RetentionPolicy.RUNTIME)]
    [Target(ElementType.METHOD)]
    public class Remove : Attribute
    {

    }
}
