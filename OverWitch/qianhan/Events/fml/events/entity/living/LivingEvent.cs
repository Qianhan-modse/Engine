using InfiniteMemories.OverWitch.qianhan.Entities;
using InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler;
using InfiniteMemories.OverWitch.qianhan.Events.fml.events.entity;

namespace Assets.OverWitch.qianhan.Events.fml.events.entity.living
{
    public class LivingEvent:EntityEvent
    {
        private EntityLivingBase entityLiving;
        public LivingEvent(EntityLivingBase entity):base(entity) 
        {
            entityLiving= entity;
        }
        public EntityLivingBase getEntityLiving()
        {
            return entityLiving;
        }
    }
    [Cancelable]
    public class LivingUpdateEvent : LivingEvent
    {
        public LivingUpdateEvent(EntityLivingBase entity) : base(entity)
        {
        }
    }
    public class LivingJumpEvent:LivingEvent
    {
        public LivingJumpEvent(EntityLivingBase entity) : base(entity)
        {
        }
    }
}
