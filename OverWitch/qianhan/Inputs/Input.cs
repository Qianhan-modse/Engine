using System.Numerics;
using System.Runtime.InteropServices;
using InfiniteMemories.OverWitch.qianhan.Inputs.InputManagers;
using InfiniteMemories.OverWitch.qianhan.Start;

namespace InfiniteMemories.OverWitch.qianhan.Inputs
{
    public class Input : InputManager
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int key);
        private readonly HashSet<int> currentMouseButtons = new HashSet<int>();
        private readonly HashSet<int> lastMouseButtons = new HashSet<int>();
        private Dictionary<string, KeyCode> keyValues = new Dictionary<string, KeyCode>();
        private HashSet<KeyCode>pressedKeys=new HashSet<KeyCode>();
        private HashSet<KeyCode>justPressedKeys=new HashSet<KeyCode>();
        private HashSet<KeyCode> justReleasedKeys = new HashSet<KeyCode>();
        private IInputEventHandler eventHandler;
        internal Vector2 mousePosition;
        private static readonly Dictionary<int, int> mouseButtonToVK = new()
        {
            { 0, 0x01 }, // Left
            { 1, 0x02 }, // Right
            { 2, 0x04 }, // Middle
            { 3, 0x05 }, // XButton1
            { 4, 0x06 }, // XButton2
            //预留扩展位
        };
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        public void UpdateMousePosition()
        {
            if (GetCursorPos(out POINT p))
            {
                mousePosition = new Vector2(p.X, p.Y);
            }
        }

        public override void setEventHandler(IInputEventHandler handler)
        {
            this.eventHandler=handler;
        }
        public override void Bind(string name, KeyCode key)
        {
            keyValues[name] = key;
        }
        [Chinese(ChinesePhase.Update)]
        public override void Update()
        {
            justPressedKeys.Clear();
           justReleasedKeys.Clear();
            foreach(var key in keyValues.Values)
            {
                if (IsKeyPressed(key))
                {
                    if (!pressedKeys.Contains(key))
                    {
                        justPressedKeys.Add(key);
                        pressedKeys.Add(key);
                        eventHandler?.onKeyPressed(key);
                    }
                }
                else {
                    if (pressedKeys.Contains(key)) 
                    {
                        justReleasedKeys.Add(key);
                        pressedKeys.Remove(key);
                        eventHandler?.onKeyReleased(key);
                    }
                }
            }
            lastMouseButtons.Clear();
            foreach (var btn in currentMouseButtons)
                lastMouseButtons.Add(btn);

            currentMouseButtons.Clear();
            foreach (var kv in mouseButtonToVK)
            {
                if ((GetAsyncKeyState(kv.Value) & 0x8000) != 0)
                {
                    currentMouseButtons.Add(kv.Key);
                }
            }
            UpdateMousePosition();
        }
        public override bool get(string name)
        {
            if (keyValues.TryGetValue(name, out KeyCode key))
            {
                return IsKeyPressed(key);
            }
            return false;
        }
        public override bool getKeyDown(string name)
        {
            if(keyValues.TryGetValue(name,out var key))
            {
                return justPressedKeys.Contains(key);
            }
            return false;
        }
        public override bool getKeyUp(string name)
        {
            if (keyValues.TryGetValue(name, out var key))
            {
                return justReleasedKeys.Contains(key);
            }
            return false;
        }
        public override bool getMouse(int button)
        {
            return IsMouseButtonPressed(button);
        }
        public override bool getMouseDown(int button)
        {
            return IsMouseButtonDown(button);
        }
        public override bool getMouseUp(int button)
        {
            return IsMouseButtonUp(button);
        }
        public override bool IsKeyPressed(KeyCode key)
        {
            //暂时留空
            return false;
        }
        public override bool IsKeyDown(KeyCode key)
        {
            return justPressedKeys.Contains(key);
        }
        public override bool IsKeyUp(KeyCode key)
        {
            return justReleasedKeys.Contains(key);
        }
        public override bool IsMouseButtonPressed(int button)
        {
            return currentMouseButtons.Contains(button);
        }
        public override bool IsMouseButtonDown(int button)
        {
            return currentMouseButtons.Contains(button) && !lastMouseButtons.Contains(button);
        }
        public override bool IsMouseButtonUp(int button)
        {
            return !currentMouseButtons.Contains(button) && lastMouseButtons.Contains(button);
        }
        public override bool getDown(string name)
        {
            //暂时留空
            return false;
        }
        public override bool getUp(string name)
        {
            //暂时留空
            return false;
        }
    }
}
