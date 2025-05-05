﻿using InfiniteMemories.OverWitch.qianhan.Entities;
using InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler;

namespace Assets.OverWitch.qianhan.Events.fml.events.entity.living
{
    //这是一个生物体治疗事件
    [Cancelable]
    public class LivingBaseHealEvent:LivingEvent
    {
        private float health;
        public LivingBaseHealEvent(EntityLivingBase entity, float heal) : base(entity)
        {
            this.setHealAmount(heal);
        }
        public float getHealAmount()
        {
            return health;
        }
        public void setHealAmount(float heal)
        {
            this.health = heal;
        }
    }
}
