using Assets.OverWitch.qianhan.Log.lang.logine;

namespace Assets.OverWitch.qianhan.Log.lang
{
    public class Debug
    {
        internal static readonly ILogger DefaultLogger = new Logger(new DebugLogHandler());
        static ILogger Logger=new Logger(new DebugLogHandler());
        public static ILogger logger => Logger;
        public static void Log(object message)
        {
            logger.Log(LogType.Log, message);
        }
        public static void Log(object message, Object context)
        {
            // 确保 message 是一个字符串类型
            string messageStr = message?.ToString() ?? string.Empty;
            logger.Log(LogType.Log, messageStr, context);
        }

        public static void LogWarning(object message)
        {
            // 确保 message 是一个字符串类型
            string messageStr = message?.ToString() ?? string.Empty;
            logger.Log(LogType.Warning, messageStr);
        }

        public static void LogWarning(object message, Object context)
        {
            // 确保 message 是一个字符串类型
            string messageStr = message?.ToString() ?? string.Empty;
            logger.Log(LogType.Warning, messageStr, context);
        }

    }
}
