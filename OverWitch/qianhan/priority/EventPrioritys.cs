using InfiniteMemories.OverWitch.qianhan.Events;
using InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler;

namespace InfiniteMemories.OverWitch.qianhan.priority
{
    public class EventPriority : IEventListener
    {
        public enum EventPrioritys
        {
            HIGHEST,
            HIGH,
            LOW,
            LOWEST
        }

        public void invoke(Event events)
        {
        }

        public void invoke(EventArgs var1)
        {
            throw new NotImplementedException();
        }
    }
}
