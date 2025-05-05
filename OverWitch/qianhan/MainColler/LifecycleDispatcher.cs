using InfiniteMemories.OverWitch.qianhan.config;
using InfiniteMemories.OverWitch.qianhan.Start;
using System.Reflection;

namespace InfiniteMemories.OverWitch.qianhan.MainColler
{
    namespace InfiniteMemories.InfiniteMemoriesEngine.All
    {
        /// <summary>
        /// 生命周期调度器，在启动时注册和调用生命周期方法
        /// </summary>
        public static class LifecycleDispatcher
        {
            private static List<(object Instance, List<MethodInfo> Methods)> instances = new();
            private const string DefaultRegistryPath = "LifecycleRegistry.yulin";

            /// <summary>
            /// 主要负责注册和调用生命周期方法
            /// </summary>
            /// <param name="pair"></param>
            /// <param name="phase"></param>
            public static void Invoke(this (object Instance, List<MethodInfo> Methods) pair, ChinesePhase phase)
            {
                foreach (var method in pair.Methods)
                {
                    var attr = method.GetCustomAttribute<ChineseAttribute>();
                    if (attr != null && (attr.Phase & phase) != 0)
                    {
                        method.Invoke(pair.Instance, null);
                    }
                }
            }
            /// <summary>
            /// 初始化生命周期调度器
            /// </summary>
            /// <param name="registryPath"></param>

            public static void InitializeFromFile(string? registryPath = null)
            {
                registryPath ??= DefaultRegistryPath;
                var classInfoList = LifecycleRegistryBuilder.LoadFromBinary(registryPath);

                foreach (var classInfo in classInfoList)
                {
                    var type = Type.GetType(classInfo.TypeName);
                    if (type == null)
                    {
                        Console.WriteLine($"[Dispatcher] Type not found: {classInfo.TypeName}");
                        continue;
                    }

                    var instance = Activator.CreateInstance(type);
                    if (instance == null)
                    {
                        Console.WriteLine($"[Dispatcher] Could not instantiate: {type.FullName}");
                        continue;
                    }

                    var lifecycle = new LifecycleInstance(instance);
                    instances.Add((instance, ResolveMethods(type, classInfo.Methods)));
                    InvokeLifecycle(lifecycle, classInfo.Registry);
                }
            }

            /// <summary>
            /// 解析方法列表,仅限程序集
            /// </summary>
            /// <param name="type"></param>
            /// <param name="methodInfos"></param>
            /// <returns></returns>
            internal static List<MethodInfo> ResolveMethods(Type type, List<LifecycleMethodInfo> methodInfos)
            {
                var result = new List<MethodInfo>();
                foreach (var methodInfo in methodInfos)
                {
                    var method = type.GetMethod(methodInfo.MethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (method != null)
                    {
                        result.Add(method);
                    }
                }
                return result;
            }
            /// <summary>
            /// 调用生命周期方法
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="reg"></param>
            private static void InvokeLifecycle(LifecycleInstance obj, LifetcycleRegisterys reg)
            {
                if (reg == LifetcycleRegisterys.SubscribeAwake) obj.Invoke("Awake");
                if (reg.HasFlag(LifetcycleRegisterys.SubscribeAwake)) obj.Invoke("Awake");
                if (reg == LifetcycleRegisterys.SubscribeStart) obj.Invoke("Start");
                if (reg.HasFlag(LifetcycleRegisterys.SubscribeStart)) obj.Invoke("Start");
                if(reg==LifetcycleRegisterys.SubscribeUpdate) obj.Invoke("Update");
                if (reg.HasFlag(LifetcycleRegisterys.SubscribeUpdate)) obj.Invoke("Update");
                if (reg == LifetcycleRegisterys.SubscribeLateUpdate) obj.Invoke("LateUpdate");
                if (reg.HasFlag(LifetcycleRegisterys.SubscribeLateUpdate)) obj.Invoke("LateUpdate");
                if (reg == LifetcycleRegisterys.SubscribeFixedUpdate) obj.Invoke("FixedUpdate");
                if (reg.HasFlag(LifetcycleRegisterys.SubscribeFixedUpdate)) obj.Invoke("FixedUpdate");
            }
            /// <summary>
            /// 调用Awake时
            /// </summary>
            public static void Awake() => instances.ForEach(i => i.Invoke(ChinesePhase.Awake));
            /// <summary>
            /// 当Awake被调用时
            /// </summary>
            public static void onAwake() => instances.ForEach(i => i.Invoke(ChinesePhase.onAwake));
            /// <summary>
            /// 当Awake结束时调用
            /// </summary>
            public static void unAwake() => instances.ForEach(i => i.Invoke(ChinesePhase.unAwake));
            /// <summary>
            /// 调用Start时
            /// </summary>
            public static void Start() => instances.ForEach(i => i.Invoke(ChinesePhase.Start));
            /// <summary>
            /// 当Start被调用时
            /// </summary>
            public static void onStart() => instances.ForEach(i => i.Invoke(ChinesePhase.onStart));
            /// <summary>
            /// 当Start结束时调用
            /// </summary>
            public static void unStart() => instances.ForEach(i => i.Invoke(ChinesePhase.unStart));
            /// <summary>
            /// 调用Update时
            /// </summary>
            public static void Update() => instances.ForEach(i => i.Invoke(ChinesePhase.Update));
            /// <summary>
            /// 当Update被调用时
            /// </summary>
            public static void onUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.onUpdate));
            /// <summary>
            /// 当Update结束时调用
            /// </summary>
            public static void unUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.unUpdate));
            /// <summary>
            /// 调用LateUpdate时
            /// </summary>
            public static void LateUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.LateUpdate));
            /// <summary>
            /// 当LateUpdate被调用时
            /// </summary>
            public static void onLateUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.onLateUpdate));
            /// <summary>
            /// 当LateUpdate结束时调用
            /// </summary>
            public static void unLateUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.unLateUpdate));
            /// <summary>
            /// 调用FixedUpdate时
            /// </summary>
            public static void FixedUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.FixedUpdate));
            /// <summary>
            /// 当FixedUpdate被调用时
            /// </summary>
            public static void onFixedUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.onFixedUpdate));
            /// <summary>
            /// 当FixedUpdate结束时调用
            /// </summary>
            public static void unFixedUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.unFixedUpdate));
            /// <summary>
            /// 当渲染被调用时
            /// </summary>
            public static void onRender() => instances.ForEach(i => i.Invoke(ChinesePhase.onRender));
            /// <summary>
            /// 当UI被调用时
            /// </summary>
            public static void onGUI() => instances.ForEach(i => i.Invoke(ChinesePhase.onGUI));
            /// <summary>
            /// 当物理被调用时
            /// </summary>
            public static void onPhysics() => instances.ForEach(i => i.Invoke(ChinesePhase.onPhysics));
            /// <summary>
            /// 当网络被调用时
            /// </summary>
            public static void onNetWorkSync() => instances.ForEach(i => i.Invoke(ChinesePhase.onNetWorkSync));
            /// <summary>
            /// 当AI时刻被调用时
            /// </summary>
            public static void AiTick() => instances.ForEach(i => i.Invoke(ChinesePhase.AiTick));
            /// <summary>
            /// 当时刻使用时
            /// </summary>
            public static void Tick() => instances.ForEach(i => i.Invoke(ChinesePhase.Tick));
            /// <summary>
            /// 当最小时刻被使用时
            /// </summary>
            public static void MinTick() => instances.ForEach(i => i.Invoke(ChinesePhase.MinTick));
            /// <summary>
            /// 当当前时刻被使用时
            /// </summary>
            public static void CurrentTick() => instances.ForEach(i => i.Invoke(ChinesePhase.CurrentTick));
            /// <summary>
            /// 当Awake被移除时
            /// </summary>
            public static void removeAwake() => instances.ForEach(i => i.Invoke(ChinesePhase.removeAwake));
            /// <summary>
            /// 当Start被移除时
            /// </summary>
            public static void removeStart() => instances.ForEach(i => i.Invoke(ChinesePhase.removeStart));
            /// <summary>
            /// 当Update被移除时
            /// </summary>
            public static void removeUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.removeUpdate));
            /// <summary>
            /// 当LateUpdate被移除时
            /// </summary>
            public static void removeLateUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.removeLateUpdate));
            /// <summary>
            /// 当FixedUpdate被移除时
            /// </summary>
            public static void removeFixedUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.removeFixedUpdate));
            /// <summary>
            /// 当所有被移除时
            /// </summary>
            public static void removeAll() => instances.ForEach(i => i.Invoke(ChinesePhase.All));
            /// <summary>
            /// 当被归属于事件的方法被调用时（最高调用权限）
            /// </summary>
            public static void Events()=>instances.ForEach(i=>i.Invoke(ChinesePhase.Events));
            /// <summary>
            /// 移除生命周期
            /// </summary>

            public static void remove()
            {
                foreach (var instance in instances)
                {
                    var type = instance.Instance.GetType();
                    var method = type.GetMethod("remove", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    method?.Invoke(instance.Instance, null);
                }
                instances.Clear();
            }
            /// <summary>
            /// 提取生命周期方法
            /// </summary>
            /// <param name="instance"></param>
            /// <returns></returns>
            internal static List<MethodInfo> ExtractChinesePhaseMethods(object instance)
            {
                return instance.GetType()
                    .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(m => m.GetCustomAttribute<ChineseAttribute>() != null)
                    .ToList();
            }
            /// <summary>
            /// 从Yulin文件初始化生命周期
            /// </summary>
            /// <param name="path"></param>
            public static void InitializeFromYulin(string path)
            {
                var infos = YulinRegistryParser.LoadFromYulinFile(path);
                foreach (var info in infos)
                {
                    var type = Type.GetType(info.TypeName);
                    if (type == null) continue;

                    var instance = Activator.CreateInstance(type);
                    if (instance == null) continue;
                    var lifecycle = new LifecycleInstance(instance);
                    instances.Add((instance, ExtractChinesePhaseMethods(instance)));
                    InvokeLifecycle(lifecycle, info.Registry);
                    //InvokeLifecycle(lifecycle, info.Registry);
                }
            }
            /// <summary>
            /// 生命周期实例类
            /// </summary>
            internal class LifecycleInstance
            {
                public object Instance;
                public Dictionary<string, MethodInfo> Methods = new();
                /// <summary>
                /// 构造函数
                /// </summary>
                /// <param name="instance"></param>
                public LifecycleInstance(object instance)
                {
                    Instance = instance;
                    var type = instance.GetType();
                    foreach (var name in new[] { "Awake", "Start", "Update", "LateUpdate", "FixedUpdate" })
                    {
                        var method = type.GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        if (method != null)
                            Methods[name] = method;
                    }
                }
                /// <summary>
                /// 调用方法
                /// </summary>
                /// <param name="method"></param>
                public void Invoke(string method)
                {
                    if (Methods.TryGetValue(method, out var mi))
                        mi.Invoke(Instance, null);
                }
            }
        }
    }

}
