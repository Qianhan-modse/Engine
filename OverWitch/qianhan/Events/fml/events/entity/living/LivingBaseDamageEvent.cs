using InfiniteMemories.OverWitch.qianhan.Entities;
using InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler;
using InfiniteMemories.OverWitch.qianhan.Util;

namespace Assets.OverWitch.qianhan.Events.fml.events.entity.living
{
    //这是一个生物体受到伤害事件
    [Cancelable]
    public class LivingBaseDamageEvent : LivingEvent
    {
        private DamageSource damageSource;
        private float damageTime;
        public LivingBaseDamageEvent(EntityLivingBase entity, DamageSource source,float amount) : base(entity)
        {
            this.damageSource = source;
            this.damageTime = amount;
        }
        public DamageSource getSource()
        {
            return damageSource;
        }
        public float getAmount()
        {
            return damageTime;
        }
        public void setAmount(float amount)
        {
            this.damageTime=amount;
        }
    }
}
