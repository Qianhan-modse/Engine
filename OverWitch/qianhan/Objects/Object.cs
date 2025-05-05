namespace InfiniteMemories.OverWitch.qianhan.Objects
{
    public class Object:MainObject
    {
        public bool isRemove;
        public bool Active { set; get; }
        public nint id { get; internal set; }

        public virtual void setActive(bool v)
        {
            this.Active = false;
        }
    }
}
