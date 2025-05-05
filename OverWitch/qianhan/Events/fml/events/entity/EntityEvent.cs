using InfiniteMemories.OverWitch.qianhan.Entities;

namespace InfiniteMemories.OverWitch.qianhan.Events.fml.events.entity
{
    public class EntityEvent : Event
    {
        public EntityEvent(Entity entity)
        {
            this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }
        public Entity getEntity()
        {
            return entity;
        }
        public override bool getCanceled() => isCanceled;
    }
    public class EntityConstructing : EntityEvent
    {
        public EntityConstructing(Entity entity) : base(entity)
        {

        }
    }
    public class CanUpdate : EntityEvent
    {
        private bool canUpdate = false;
        public bool getCanUpdate() { return canUpdate; }
        public CanUpdate(Entity entity) : base(entity)
        {
        }
        public void setCanUpdate(bool v)
        {
            this.canUpdate = v;
        }
    }
}
