using InfiniteMemories.OverWitch.qianhan.Metas;
using InfiniteMemories.OverWitch.qianhan.MonoBehaviours;

namespace InfiniteMemories.OverWitch.qianhan.MainColler.Colliders
{
    public static class ColliderRegistry
    {
        private static readonly List<Collider> _colliders = new();
        private static readonly Dictionary<int, List<Collider>> _layerColliders = new();
        private static LayerMask Layer;
        public static IReadOnlyList<Collider> All => _colliders;

        public static void Register(Collider col)
        {
            if (col == null || col.isRemove || !col.Active) return;

            _colliders.Add(col);
            int layer = col.Layer.value;
            if (!_layerColliders.TryGetValue(layer, out var list))
            {
                list = new List<Collider>();
                _layerColliders[layer] = list;
            }
            list.Add(col);
        }

        public static void Unregister(Collider col)
        {
            if (col == null) return;

            _colliders.Remove(col);
            if (_layerColliders.TryGetValue(col.Layer.value, out var list))
                list.Remove(col);
        }

        public static IEnumerable<Collider> QueryByLayer(LayerMask mask)
        {
            foreach (var kv in _layerColliders)
            {
                if ((mask.value & kv.Key) != 0)
                {
                    foreach (var c in kv.Value)
                        yield return c;
                }
            }
        }
    }
}
