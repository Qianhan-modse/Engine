using System.Runtime.Serialization.Formatters.Binary;

namespace InfiniteMemories.OverWitch.qianhan.config
{
    public static class YulinToBinaryConverter
    {
        public static void ConvertYulinToBinary(string yulinPath, string binaryPath)
        {
            var registryList = YulinRegistryParser.LoadFromYulinFile(yulinPath); // 你已有的解析器
            using var fs = new FileStream(binaryPath, FileMode.Create);
#pragma warning disable SYSLIB0011
            new BinaryFormatter().Serialize(fs, registryList);
#pragma warning restore SYSLIB0011
        }
    }

}
