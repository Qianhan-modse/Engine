using InfiniteMemories.OverWitch.qianhan.annotations.meta;

namespace InfiniteMemories.OverWitch.qianhan.annotations
{
    [AttributeUsage(AttributeTargets.All)]
    [VisibleToOtherModules]
    public class StaticAccessorAttribute : Attribute, BindingsAttribute
    {
        public string Name { get; set; }

        public StaticAccessorType Type { get; set; }

        public StaticAccessorAttribute()
        {
        }

        [VisibleToOtherModules]
        internal StaticAccessorAttribute(string name)
        {
            Name = name;
        }

        public StaticAccessorAttribute(StaticAccessorType type)
        {
            Type = type;
        }

        public StaticAccessorAttribute(string name, StaticAccessorType type)
        {
            Name = name;
            Type = type;
        }
    }
}
