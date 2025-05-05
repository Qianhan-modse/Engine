using InfiniteMemories.OverWitch.qianhan.annotations;
using InfiniteMemories.OverWitch.qianhan.Entities;
using InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler;
using InfiniteMemories.OverWitch.qianhan.Item;

namespace Assets.OverWitch.qianhan.Events.fml.events.entity.living
{
    public class LivingBaseEntityUseItemEvent:LivingEvent
    {
        private ItemStack stack;
        private int duration;
        public LivingBaseEntityUseItemEvent(EntityLivingBase entity, ItemStack stack, int duration) : base(entity)
        {
            this.stack = stack;
            this.setDuration(duration);
        }
        [Nonnull]
        public ItemStack getItem() { return stack; }
        public int getDuration() { return duration; }
        public void setDuration(int duration) { this.duration = duration; }
        [Cancelable]
        public class Start:LivingBaseEntityUseItemEvent
        {
            public Start(EntityLivingBase entity, [Nonnull]ItemStack itemStack,int duration):base(entity,itemStack,duration)
            { }
        }
        [Cancelable]
        public class Tick:LivingBaseEntityUseItemEvent
        {
            public Tick(EntityLivingBase entity, [Nonnull] ItemStack itemStack, int duration) : base(entity, itemStack, duration)
            { }
        }
        [Cancelable]
        public class Stop:LivingBaseEntityUseItemEvent
        {
            public Stop(EntityLivingBase entity, [Nonnull] ItemStack itemStack, int duration) : base(entity, itemStack, duration)
            { }
        }
        public class Finish:LivingBaseEntityUseItemEvent
        {
            private ItemStack result;
            public Finish(EntityLivingBase entity, [Nonnull] ItemStack itemStack, int duration, [Nonnull] ItemStack result) : base(entity, itemStack, duration)
            {
                
            }
            [Nonnull]
            public ItemStack getResultStack()
            {
                return result;
            }
            public void setResultStack([Nonnull] ItemStack result)
            {
                this.result = result;
            }
        }
    }
}
