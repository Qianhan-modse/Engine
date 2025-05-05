namespace InfiniteMemories.OverWitch.qianhan.annotations
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    [VisibleToOtherModules]
    public class VisibleToOtherModulesAttribute : Attribute
    {
        public VisibleToOtherModulesAttribute() { }
        public VisibleToOtherModulesAttribute(params string[] modules) { }
    }
}
