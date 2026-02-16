using UnityEngine;

namespace Base.Manager
{
    public class BoxCollider : ICollider
    {
        public IEntity Owner { get; set; }
        public ColliderShape Shape => ColliderShape.Box;
        public Vector2 Center { get; set; }
        public Vector2 HalfSize { get; set; }
        public Rect AABB => new Rect(Center.x - HalfSize.x, Center.y - HalfSize.y, HalfSize.x * 2, HalfSize.y * 2);

        public BoxCollider(IEntity owner, Vector2 center, Vector2 halfSize)
        {
            Owner = owner;
            Center = center;
            HalfSize = halfSize;
        }
    }
}