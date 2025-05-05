namespace InfiniteMemories.OverWitch.qianhan.Item
{
    public class ItemStack : item
    {
        public int count;
        public int maxCount;
        public string name;
        public ItemStack(string name, int count)
        {
            this.name = name;
            this.count = count;
            this.maxCount = 64;
        }
        public ItemStack(string name, int count, int maxCount)
        {
            this.name = name;
            this.count = count;
            this.maxCount = maxCount;
        }

        public ItemStack()
        {
        }

        public void setItemName(string name)
        {
            this.name = name;
        }
        public string getItemName()
        {
            return this.name;
        }
        public override void onItemUpdate()
        {
        }
    }
}
