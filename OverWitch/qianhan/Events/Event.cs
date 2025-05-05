using InfiniteMemories.OverWitch.qianhan.Entities;
using InfiniteMemories.OverWitch.qianhan.Events.fml.common.eventhandler;
using InfiniteMemories.OverWitch.qianhan.priority;
using InfiniteMemories.OverWitch.qianhan.Start;

namespace InfiniteMemories.OverWitch.qianhan.Events
{
    /// <summary>
    /// 事件类型，用于监听事件并阻止其发生
    /// </summary>
    public class Event
    {
        public AnnotationEvent annotationEvent = AnnotationEvent.None;
        private static readonly Dictionary<Type, List<Delegate>> eventListeners = new Dictionary<Type, List<Delegate>>();
        protected Entity entity;
        protected bool isCanceled;
        protected bool isGlobalMark;
        public Event()
        {
            
            entity = new Entity();
            setUp();
            isCanceled = false;
            isGlobalMark = false;
            checkAnnotationFlags();
        }
        protected virtual void checkAnnotationFlags()
        {
            if ((annotationEvent & AnnotationEvent.GlobalCancel) != 0 || (annotationEvent & AnnotationEvent.GlobalRemove) != 0)
            {
                isCanceled = true;
                isGlobalMark = true;
            }
        }
        public static void Annotation(Event @event, AnnotationEvent annotationEvent)
        {
            if ((annotationEvent & AnnotationEvent.Cancel) != 0)
            {
                @event.setCanceled(true);
            }
            if ((annotationEvent & AnnotationEvent.GlobalSetCancel) != 0)
            {
                //取消全局事件，全局事件只有是false才不会被取消
                @event.setGlobalMark(true);
            }
        }
        //两个阻止的逻辑，一个是setEvent这个布尔值，一个是setCanceled
        //但setEvent要比setCanceled更好，最起码setEvent可以管全局事件
        /// <summary>
        /// 全局事件开关，如果是false才不会影响全部事件的发生
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool setGlobalMark(bool v)
        {
            if (entity.forceDead)
            {
                return false;
            }
            else
                isCanceled = true;
            return isGlobalMark = v;
        }
        /// <summary>
        /// Event事件只需要在初始化之前注册就行了
        /// </summary>
        [Chinese(ChinesePhase.Events)]
        public virtual void onEventAlave()
        {

        }
        public virtual void setCanceled(bool E)
        {
            if (!isGlobalMark)
            {
                if (entity.forceDead)
                {
                    return;
                }
                else
                {
                    isCanceled = E;
                }
            }
            else { return; }
        }
        public enum Result
        {
            DENY,
            DEFAULT,
            ALLOW
        }
        public bool onCanceled() { return isCanceled; }
        public bool isCancelable() { return false; }
        private Result result = Result.DEFAULT;
        private EventPriority? phase = null;
        public bool getEvent() => !isGlobalMark;
        public bool getGlobalMarkEvent() => !isGlobalMark;
        public bool hasResult() => result != Result.DEFAULT;
        public bool IsEventEnabled() => !isGlobalMark;
        public bool isGlobalMarEvent() => isGlobalMark;
        public bool isCanceledEvent() => isCanceled;
        public virtual bool getCanceled() { return isCanceled; }
        public Result getResult()
        {
            return result;
        }
        public virtual void setResult(Result results)
        {
            result = results;
        }
        protected void setUp()
        {

        }
        public EventPriority getPhase()
        {
            return phase;
        }
    }
}
