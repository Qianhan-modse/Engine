using Assets.OverWitch.qianhan.Events.fml.events.entity.living;
using InfiniteMemories.OverWitch.qianhan.Entities;
using InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler;
using InfiniteMemories.OverWitch.qianhan.Util;

namespace EntityLivingBaseEvent
{
    /// <summary>
    /// ��������¼���������������ʱ��Ч���Ա�����ʹ��������Ч
    /// </summary>
    [Cancelable]
    public class LivingBaseDeathEvent :LivingEvent
    {
        public DamageSource source;
        public LivingBaseDeathEvent(EntityLivingBase entity,DamageSource Source) : base(entity)
        {
            this.source = Source;
            this.isCanceled = false;
        }
        //�����ⲿ���getSource����
        public DamageSource GetSource()
        {
            return source;
        }
    }
}