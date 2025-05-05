using System.Reflection;
using InfiniteMemories.OverWitch.qianhan.Start;

namespace InfiniteMemories.OverWitch.qianhan.MainColler
{
    /// <summary>
    /// 无状态机的组件接口
    /// </summary>
    public static class LifecycleInvoker
    {
        public static void InvokeByKeyword(object obj, string keyword)
        {
            var methods = obj.GetType()
                             .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var method in methods)
            {
                if (method.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) &&
                    method.GetParameters().Length == 0)
                {
                    method.Invoke(obj, null);
                }
            }
        }

        public static void InvokeStart(object obj) => InvokeByKeyword(obj, "Start");
        public static void InvokeUpdate(object obj) => InvokeByKeyword(obj, "Update");
    }
}
namespace OverWitch.qianhan.Util
{
    /// <summary>
    /// 生命周期方法调用器，自动调用对象的生命周期方法
    /// </summary>
    public class LifecycleInvoker
    {
        private static ILifecycleContextProvider? contextProvider;
        private static Dictionary<Type, List<MethodInfo>> cache = new();

        public static void SetContextProvider(ILifecycleContextProvider provider)
        {
            contextProvider = provider;
        }

        private static List<MethodInfo> GetCachedMethods(Type type)
        {
            if (cache.TryGetValue(type, out var methods)) return methods;
            string[] lifecycleKeywords = { "Awake", "Start", "Update", "LateUpdate", "FixedUpdate" };
            methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
              .Where(m => lifecycleKeywords.Any(k =>
                  m.Name.Contains(k, StringComparison.OrdinalIgnoreCase)))
              .ToList();
            cache[type] = methods;
            return methods;
        }
        public static void InvokeByPhase(object obj, ChinesePhase phase)
        {
            if (contextProvider == null)
                throw new InvalidOperationException("ContextProvider is not set.");

            // 判断各个阶段的标志位
            if ((phase & ChinesePhase.Awake) != 0)
                InvokeAwake(obj);
            if ((phase & ChinesePhase.Start) != 0)
                InvokeStart(obj);
            if ((phase & ChinesePhase.Update) != 0)
                InvokeUpdate(obj);
            if ((phase & ChinesePhase.LateUpdate) != 0)
                InvokeLateUpdate(obj);
            if ((phase & ChinesePhase.FixedUpdate) != 0)
                InvokeFixedUpdate(obj);
        }
        public static void InvokeAwake(object obj) => InvokeByKeyword(obj, "Awake");
        public static void InvokeStart(object obj) => InvokeByKeyword(obj, "Start");
        public static void InvokeUpdate(object obj) => InvokeByKeyword(obj, "Update");
        public static void InvokeLateUpdate(object obj) => InvokeByKeyword(obj, "LateUpdate");
        public static void InvokeFixedUpdate(object obj) => InvokeByKeyword(obj, "FixedUpdate");

        private static void InvokeByKeyword(object obj, string keyword)
        {
            if (contextProvider == null)
                throw new InvalidOperationException("ContextProvider is not set.");

            foreach (var method in GetCachedMethods(obj.GetType()))
            {
                if (!method.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)) continue;

                var parameters = method.GetParameters();
                var args = new object?[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    args[i] = contextProvider.GetContext(parameters[i].ParameterType);
                }

                try
                {
                    method.Invoke(obj, args);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[LifecycleInvoker] Error in {method.Name}: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
        }
    }
}
namespace OverWitch.qianhan.Util
{
    public interface ILifecycleContextProvider
    {
        object? GetContext(Type type);
    }

    public class DefaultLifecycleContextProvider : ILifecycleContextProvider
    {
        private readonly Dictionary<Type, object> contextMap = new();

        public void Register<T>(T instance)
        {
            if (instance != null)
                contextMap[typeof(T)] = instance;
        }

        public object? GetContext(Type type)
        {
            foreach (var kv in contextMap)
            {
                if (type.IsAssignableFrom(kv.Key))
                    return kv.Value;
            }

            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }

}
