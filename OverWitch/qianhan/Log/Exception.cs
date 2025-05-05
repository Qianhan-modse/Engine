using Assets.OverWitch.qianhan.Log.lang;

namespace Assets.OverWitch.qianhan.Log
{
    using System.Diagnostics;

    public class Exception : Throwable
    {
        static long serialVersionUID = -338751699312422948L;

        // 构造函数
        public Exception() { }

        public Exception(string message) { }

        public Exception(string message, Throwable cause) { }

        public Exception(Throwable cause) { }

        public Exception(string message, Throwable cause, bool enableSuppression, bool writableStackTrace) { }

        // StackTrace 属性：捕获堆栈跟踪信息
        public string StackTrace
        {
            get
            {
                var stackTrace = new StackTrace(true); // 设置 true 以包含文件信息
                return stackTrace.ToString(); // 返回完整的堆栈信息
            }
        }
    }

}
