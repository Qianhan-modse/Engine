using InfiniteMemories.OverWitch.qianhan.Entities;
using InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler;
using InfiniteMemories.OverWitch.qianhan.PotionEffects;

namespace Assets.OverWitch.qianhan.Events.fml.events.entity.living
{
    [Cancelable]
    public class LivingUpdatePotionEffectEvent : LivingEvent
    {
        private PotionEffect effect;
        public LivingUpdatePotionEffectEvent(EntityLivingBase entity, PotionEffect effect) : base(entity)
        {
            this.effect = effect;
        }
        public PotionEffect getEffect()
        {
            return effect;
        }
    }
}