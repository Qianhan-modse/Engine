using InfiniteMemories.OverWitch.qianhan.annotations.meta;

namespace InfiniteMemories.OverWitch.qianhan.annotations
{
    [Documented]
    [TypeQualifier]
    [Retention(RetentionPolicy.RUNTIME)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class NonnullAttribute : Attribute
    {
        public When when;

        public When WhenValue { get; }

        public NonnullAttribute(When when = When.ALWAYS)
        {
            WhenValue = when;
        }

        public class Checker : ITypeQualifierValidator<NonnullAttribute>
        {
            public When ForConstantValue(NonnullAttribute qualifierArgument, object value)
            {
                return value == null ? When.NEVER : When.ALWAYS;
            }
        }
    }

    // 定义接口，类似于 Java 中的 TypeQualifierValidator<T>
    public interface ITypeQualifierValidator<T>
    {
        When ForConstantValue(T qualifierArgument, object value);
    }
}
