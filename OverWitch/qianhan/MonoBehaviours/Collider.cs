using InfiniteMemories.OverWitch.qianhan.Entities;
using InfiniteMemories.OverWitch.qianhan.Metas;
using InfiniteMemories.OverWitch.qianhan.Objects.Vectors;

namespace InfiniteMemories.OverWitch.qianhan.MonoBehaviours
{
    public class Collider : Component
    {
        public required Vector3 Position;
        internal float Radius;
        public readonly LayerMask Layer;
        public readonly Entity entitys;
        public Collider(string name, string description, bool active, bool remove, Entity entity) : base(name, description, active, remove, entity)
        {
            entitys = entity;
        }

        public bool isOpen { get; set; }
        public Vector3 Center { get; internal set; }

        public virtual bool Intersects(Ray ray, out float distance)
        {
            distance = 0f;
            return true;
        }
    }
}
