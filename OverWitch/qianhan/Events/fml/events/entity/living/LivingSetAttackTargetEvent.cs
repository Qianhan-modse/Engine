using InfiniteMemories.OverWitch.qianhan.Entities;

namespace Assets.OverWitch.qianhan.Events.fml.events.entity.living
{
    public class LivingSetAttackTargetEvent:LivingEvent
    {
        private EntityLivingBase target;
        public LivingSetAttackTargetEvent(EntityLivingBase entityLiving, EntityLivingBase target) : base(entityLiving)
        {
            this.target = target;
        }
        public EntityLivingBase getTarget() { return target; }
    }
}
