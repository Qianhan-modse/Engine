using InfiniteMemories.OverWitch.qianhan.Metas;
using InfiniteMemories.OverWitch.qianhan.MonoBehaviours;
using InfiniteMemories.OverWitch.qianhan.Objects.Vectors;

namespace InfiniteMemories.OverWitch.qianhan.MainColler.Colliders
{
    public static class Physics
    {
        public static List<Collider> Colliders { get; } = new();

        public static bool Raycast(Vector3 origin, Vector3 direction, float range, out RaycastHit hitInfo)
        {
            hitInfo = new RaycastHit();
            float closestDist = float.MaxValue;
            Collider? hitCollider = null;

            foreach (var collider in Colliders)
            {
                if (collider.Intersects(new Ray(origin, direction), out float distance))
                {
                    if (distance <= range && distance < closestDist)
                    {
                        closestDist = distance;
                        hitCollider = collider;
                    }
                }
            }

            if (hitCollider != null)
            {
                hitInfo = new RaycastHit(
                    hitCollider.entitys,
                    origin + direction * closestDist,
                    direction * -1, // 修复：直接使用方向向量的反向  
                    closestDist
                );
                return true;
            }

            return false;
        }

        public static bool Raycast(Vector3 origin, Vector3 direction, float range)
        {
            return Raycast(origin, direction, range, out _);
        }

        public static bool CheckSphere(Vector3 position, float radius, LayerMask layerMask)
        {
            foreach (var collider in Colliders)
            {
                if (!collider.Active || collider.isRemove) continue;

                // 可加 layerMask 筛选  
                if ((layerMask.value & collider.Layer.value) == 0) continue;

                float distance = Vector3.Distance(position, collider.Center);
                if (distance <= radius + collider.Radius)
                    return true;
            }

            return false;
        }
    }
}
