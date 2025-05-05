using System.Diagnostics;

namespace Assets.OverWitch.qianhan.Log
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class IllegalArgumentException
    {
        public IllegalArgumentException(string v)
        {
        }
        public IllegalArgumentException()
        {
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}