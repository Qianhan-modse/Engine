using InfiniteMemories.OverWitch.qianhan.Entities;

namespace InfiniteMemories.OverWitch.qianhan.MonoBehaviours
{
    public class Component:Objects.Object
    {
        public int Key { get; set; } = 0;//初始值为0
        public string Name { get; set; } = "Component";
        public string Description { get; set; } = "This is a component.";
        public bool isOpent { get; set; } = false;

        public Entity Owner { get; internal set; } // 绑定的实体引用

        public virtual void onAdd() { }
        public virtual void onRemove() { }
        public virtual void onComponentUpdate() { }
        public Component(string name, string description, bool active, bool remove, Entity entity)
        {
            this.Name = name;
            this.Description = description;
            this.Active = active;
            this.isRemove = remove;
            this.Owner = entity;
        }
        public Component(int key, string name, string description, bool isOpent, Entity owner)
        {
            Key = key;
            Name = name;
            Description = description;
            this.isOpent = isOpent;
            Owner = owner;
        }
    }
}
