

using InfiniteMemories.OverWitch.qianhan.Log.lang.logine;
using InfiniteMemories.OverWitch.qianhan.Numbers;

namespace Assets.OverWitch.qianhan.Log
{
    public class Integer : Number, Comparable<Integer>
    {
        public static int CURRENT_VALUE;
        public static int MIN_VALUE = 8000000;
        public static int MAX_VALUE = (int)500000000;
        public override double doubleValue()
        {
            return (double)CURRENT_VALUE;
        }

        public override float floatValue()
        {
            return (float)CURRENT_VALUE;
        }

        public override int inValue()
        {
            return (int)CURRENT_VALUE;
        }

        public override long longValue()
        {
            return CURRENT_VALUE;
        }

        int Comparable<Integer>.compareTo(Integer o)
        {
            return 0;
        }
    }
}
