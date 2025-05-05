using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using InfiniteMemories.OverWitch.qianhan.Inputs;
using InfiniteMemories.OverWitch.qianhan.Start;

namespace InfiniteMemories.OverWitch.qianhan.Buttions
{
    public static class Mouse
    {
        public static Vector2 Position { get; private set; }
        public static bool isLeftDown { get; private set; }
        public static bool isRightDown { get; private set; }
        public static bool isLeftUp { get; private set; }
        public static bool isRightUp { get; private set; }
        public static bool isLeftPressed { get; private set; }
        public static bool isRightPressed { get; private set; }
        private static Input Input;
        [Chinese(ChinesePhase.Update)]
        public static void Update()
        {
            Position = Input.mousePosition;
            isLeftDown = Input.getMouseDown(0);
        }
    }
}
