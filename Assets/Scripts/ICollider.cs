using UnityEngine;

namespace Base.Manager
{
    public interface ICollider
    {
        public IEntity Owner { get; set; }
        public ColliderShape Shape { get; }
        public Vector2 Center { get; set; }
        public Rect AABB { get; }
    }
}