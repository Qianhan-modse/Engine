using System.Reflection;
using InfiniteMemories.OverWitch.qianhan.Start;

namespace InfiniteMemories.OverWitch.qianhan.MainColler
{
    /// <summary>
    /// 生命周期注册信息构建器，用于扫描程序集中的订阅的生命周期注册信息
    /// </summary>
    public static class LifecycleRegistryBuilder
    {
        /// <summary>
        /// 从文件加载生命周期注册信息
        /// </summary>
        public static List<LifecycleRegistryInfo> LoadFromBinary(string path)
        {
            // 使用二进制读取注册信息
            try
            {
                using var stream = File.OpenRead(path);
                return DeserializeFromBinary(stream) ?? new List<LifecycleRegistryInfo>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to load registry: {ex.Message}");
                return new List<LifecycleRegistryInfo>();
            }
        }

        /// <summary>
        /// 将生命周期注册信息保存到文件
        /// </summary>
        public static void SaveToBinary(string path, IEnumerable<Type> types)
        {
            var registryList = types.Select(type => new LifecycleRegistryInfo
            {
                TypeName = type.FullName,
                Methods = GetLifecycleMethods(type).ToList()
            }).ToList();

            // 将生命周期注册信息序列化为二进制并保存
            try
            {
                using var stream = new FileStream(path, FileMode.Create);
                SerializeToBinary(stream, registryList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to save registry: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取一个类型的生命周期方法信息
        /// </summary>
        private static IEnumerable<LifecycleMethodInfo> GetLifecycleMethods(Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.GetCustomAttributes(typeof(ChineseAttribute), false).Any())
                .Select(m => new LifecycleMethodInfo
                {
                    MethodName = m.Name,
                    Phase = ((ChineseAttribute)m.GetCustomAttributes(typeof(ChineseAttribute), false).First()).Phase
                });
        }

        /// <summary>
        /// 从二进制流反序列化
        /// </summary>
        private static List<LifecycleRegistryInfo> DeserializeFromBinary(Stream stream)
        {
            using (var reader = new BinaryReader(stream))
            {
                var length = reader.ReadInt32();
                var registryList = new List<LifecycleRegistryInfo>();

                for (int i = 0; i < length; i++)
                {
                    var typeName = reader.ReadString();
                    var methodCount = reader.ReadInt32();
                    var methods = new List<LifecycleMethodInfo>();

                    for (int j = 0; j < methodCount; j++)
                    {
                        var methodName = reader.ReadString();
                        var phase = (ChinesePhase)reader.ReadInt32();
                        methods.Add(new LifecycleMethodInfo { MethodName = methodName, Phase = phase });
                    }

                    registryList.Add(new LifecycleRegistryInfo { TypeName = typeName, Methods = methods });
                }

                return registryList;
            }
        }

        /// <summary>
        /// 将生命周期注册信息序列化为二进制流
        /// </summary>
        private static void SerializeToBinary(Stream stream, List<LifecycleRegistryInfo> registryList)
        {
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(registryList.Count);

                foreach (var registry in registryList)
                {
                    writer.Write(registry.TypeName);
                    writer.Write(registry.Methods.Count);

                    foreach (var method in registry.Methods)
                    {
                        writer.Write(method.MethodName);
                        writer.Write((int)method.Phase); // Convert phase to int for storage
                    }
                }
            }
        }
    }

    // 生命周期注册信息
    [Serializable]
    public class LifecycleRegistryInfo
    {
        public string TypeName { get; set; }
        public List<LifecycleMethodInfo> Methods { get; set; }
        public LifetcycleRegisterys Registry { get; internal set; }
    }
    [Serializable]
    public class LifecycleClassInfo
    {
        public string TypeName { get; set; } = "";
        public LifetcycleRegisterys Registry { get; set; }

        // 新增字段：用于记录每个方法及其对应的 Phase
        public List<LifecycleMethodInfo> Methods { get; set; } = new();
    }

    // 生命周期方法信息
    [Serializable]
    public class LifecycleMethodInfo
    {
        public string MethodName { get; set; }
        public ChinesePhase Phase { get; set; }
    }
}
