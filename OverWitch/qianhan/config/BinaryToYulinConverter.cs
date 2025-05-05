using System.Runtime.Serialization.Formatters.Binary;
using InfiniteMemories.OverWitch.qianhan.MainColler;

namespace InfiniteMemories.OverWitch.qianhan.config
{
    public static class BinaryToYulinConverter
    {
        public static void ConvertBinaryToYulin(string binaryPath, string yulinPath)
        {
            using var fs = new FileStream(binaryPath, FileMode.Open);
#pragma warning disable SYSLIB0011
            var list = (List<LifecycleClassInfo>)new BinaryFormatter().Deserialize(fs);
#pragma warning restore SYSLIB0011
            using var writer = new StreamWriter(yulinPath);

            foreach (var info in list)
            {
                writer.WriteLine($"- TypeName: {info.TypeName}");
                writer.WriteLine($"  Registry: {info.Registry}");
                writer.WriteLine("  Methods:");
                foreach (var method in info.Methods)
                {
                    writer.WriteLine($"    - MethodName: {method.MethodName}");
                    writer.WriteLine($"      Phase: {method.Phase}");
                }
                writer.WriteLine();
            }
        }
    }
}
