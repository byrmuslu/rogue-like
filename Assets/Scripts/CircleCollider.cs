using UnityEngine;

namespace Base.Manager
{
    public class CircleCollider : ICollider
    {
        public IEntity Owner { get; set; }
        public ColliderShape Shape => ColliderShape.Circle;
        public Vector2 Center { get; set; }
        public float Radius { get; set; }
        public Rect AABB => new Rect(Center.x - Radius, Center.y - Radius, Radius * 2, Radius * 2);

        public CircleCollider(IEntity owner, Vector2 center, float radius)
        {
            Owner = owner;
            Center = center;
            Radius = radius;
        }
    }
}