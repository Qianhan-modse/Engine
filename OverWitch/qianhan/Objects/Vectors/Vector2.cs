namespace InfiniteMemories.OverWitch.qianhan.Objects.Vectors
{
    public class Vector2
    {
        public float x { get; set; }
        public float y { get; set; }
        public static Vector2 Zero { get; internal set; }

        public Vector2(float X, float Y)
        {
            x = X;
            y = Y;
        }

        // 加法
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        // 减法
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        // 乘法
        public static Vector2 operator *(Vector2 v, float scalar)
        {
            return new Vector2(v.x * scalar, v.y * scalar);
        }

        // 除法
        public static Vector2 operator /(Vector2 v, float scalar)
        {
            if (scalar == 0)
                throw new DivideByZeroException("Cannot divide by zero.");
            return new Vector2(v.x / scalar, v.y / scalar);
        }

        // 点积
        public float Dot(Vector2 v)
        {
            return x * v.x + y * v.y;
        }

        // 长度
        public float Length()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        // 归一化
        public Vector2 Normalize()
        {
            float length = Length();
            if (length > 0)
            {
                return this / length;
            }
            return new Vector2(0, 0);
        }

        // 转换为字符串
        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}
