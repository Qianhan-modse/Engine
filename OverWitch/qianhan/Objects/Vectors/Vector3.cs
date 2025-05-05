using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteMemories.OverWitch.qianhan.Objects.Vectors
{
    public class Vector3
    {
        public static Vector3 down { set; get; }
        private int v1;
        private float v2;

        public double x { set; get; }
        public double y { set; get; }
        public double z { set; get; }
        public static Vector3 Zero { get; set; }
        public static Vector3 One { get; set; }
        public static Vector3 Down { get; set; }
        public static Vector3 Up { get; set; }

        public Vector3(double X, double Y, double Z)
        {
            x = X;
            y = Y;
            z = Z;
        }
        public static implicit operator Vector3((int x, int y, int z) tuple)
           => new Vector3(tuple.x, tuple.y, tuple.z);

        public static implicit operator Vector3((float x, float y, float z) tuple)
            => new Vector3(tuple.x, tuple.y, tuple.z);

        public static implicit operator Vector3((double x, double y, double z) tuple)
            => new Vector3(tuple.x, tuple.y, tuple.z);
        // 加法
        public static Vector3 operator +(Vector3 a, Vector3 b)
            => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);

        public static Vector3 operator -(Vector3 a, Vector3 b)
            => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);

        public static Vector3 operator *(Vector3 v, double scalar)
            => new Vector3(v.x * scalar, v.y * scalar, v.z * scalar);

        public static Vector3 operator /(Vector3 v, double scalar)
        {
            if (scalar == 0)
                throw new DivideByZeroException("Cannot divide by zero.");
            return new Vector3(v.x / scalar, v.y / scalar, v.z / scalar);
        }

        public float Length()
            => (float)Math.Sqrt(x * x + y * y + z * z);
        public float Dot(Vector3 v)
           => (float)(x * v.x + y * v.y + z * v.z);

        public Vector3 Cross(Vector3 v)
            => new Vector3(
                y * v.z - z * v.y,
                z * v.x - x * v.z,
                x * v.y - y * v.x
            );
        /// <summary>
        /// 考虑到针对Unity-style的兼容性，此方法保留，建议使用实例.Normalize()代替;
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Vector3 Normalize(Vector3 direction)
        {
            return direction.Normalize();
        }

        public static float Distance(Vector3 a, Vector3 b)
        {
            double dx = a.x - b.x;
            double dy = a.y - b.y;
            double dz = a.z - b.z;
            return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public static Vector3 ProjectOnPlane(Vector3 movement, Vector3 normal)
        {
            var norm = normal.Normalize();
            var dot = movement.Dot(norm);
            return movement - norm * dot;
        }

        public override string ToString()
            => $"({x:F2}, {y:F2}, {z:F2})";

        public Vector3? Normalize()
        {
            float length = Length();
            if (length > 0)
            {
                return this / length;
            }
            return new Vector3(0, 0, 0);
        }
        public void Lerp(Vector3 vector, Vector3 vector1, float e)
        {

        }
    }
    public struct Ray
    {
        public Vector3 origins;
        public Vector3 directions;
        public Ray(Vector3 position, Vector3 direction)
        {
            origins = position;
            directions = direction.Normalize(); // Fix: Corrected to use instance method Normalize()  
        }
    }
}
