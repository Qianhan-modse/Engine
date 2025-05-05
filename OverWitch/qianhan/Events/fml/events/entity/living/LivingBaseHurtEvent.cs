using InfiniteMemories.OverWitch.qianhan.Entities;
using InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler;
using InfiniteMemories.OverWitch.qianhan.Util;

namespace Assets.OverWitch.qianhan.Events.fml.events.entity.living
{
    //这是一个生物体受伤事件
    [Cancelable]
    public class LivingBaseHurtEvent:LivingEvent
    {
        private DamageSource damageSource;
        private float damageAmount;
        public LivingBaseHurtEvent(EntityLivingBase entityLiving,DamageSource source,float amount):base(entityLiving)
        {
            this.damageSource = source;
            this.damageAmount = amount;
        }
        public DamageSource GetSource()
        {
            return damageSource;
        }
        public float getAmount()
        {
            return damageAmount;
        }
        public void setAmount(float amount)
        {
            this.damageAmount = amount;
        }
    }
}
