namespace InfiniteMemories.OverWitch.qianhan.Attributes
{
    /// <summary>
    /// 小写命名合法，不归属命名规范，而是为了和DllImportAttribute区分
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class import : Attribute
    {
        public string DllName { get; }
        /// <summary>
        /// 简写的，还未完全实现
        /// </summary>
        /// <param name="name"></param>
        public import(string name)
        {
            DllName = name;
        }
    }
}
