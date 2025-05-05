using InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler;
using InfiniteMemories.OverWitch.qianhan.Events.fml;
using InfiniteMemories.OverWitch.qianhan.Events;
using Assets.OverWitch.qianhan.Log.lang;

namespace InfiniteMemories.OverWitch.qianhan.annotations
{
    public interface IEventExceptionHandler
    {
        void handleException(EventBus bus, Event @event, IEventListener[] listeners, int index, Throwable throwable);
        void handleException(EventBus eventBus, Event eventInstance, IEventListener[] eventListeners, int index, Exception ex);
        void handleException(Exception ex);
    }
}
