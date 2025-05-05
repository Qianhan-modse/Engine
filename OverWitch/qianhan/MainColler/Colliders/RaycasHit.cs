using InfiniteMemories.OverWitch.qianhan.Entities;
using InfiniteMemories.OverWitch.qianhan.Objects.Vectors;

namespace InfiniteMemories.OverWitch.qianhan.MainColler.Colliders
{
    public class RaycastHit
    {
        public Entity? HitEntity { get; internal set; }        // 被命中的实体
        public Vector3 Point { get; internal set; }            // 命中点
        public Vector3 Normal { get; internal set; }           // 命中面法线
        public float Distance { get; internal set; }           // 命中距离
        public bool Hit => HitEntity != null;

        public RaycastHit() { }

        public RaycastHit(Entity hitEntity, Vector3 point, Vector3 normal, float distance)
        {
            HitEntity = hitEntity;
            Point = point;
            Normal = normal;
            Distance = distance;
        }
    }
}
