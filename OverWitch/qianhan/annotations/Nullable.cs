using InfiniteMemories.OverWitch.qianhan.annotations.meta;

namespace InfiniteMemories.OverWitch.qianhan.annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    [TypeQualifierNickname]
    [Retention(RetentionPolicy.RUNTIME)]
    [Documented]
    [Nonnull(when = When.UNKNOWN)]

    public class Nullable : Attribute
    {
    }
}
