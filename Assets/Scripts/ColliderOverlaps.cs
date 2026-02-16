using UnityEngine;

namespace Base.Manager
{
    public static class ColliderOverlaps
    {
        public static bool Overlaps(ICollider collider1, ICollider collider2)
        {
            if (!collider1.AABB.Overlaps(collider2.AABB))
            {
                return false;
            }

            return (collider1.Shape, collider2.Shape) switch
            {
                (ColliderShape.Circle, ColliderShape.Circle) => CircleCircle(collider1 as CircleCollider,
                    collider2 as CircleCollider),
                (ColliderShape.Box, ColliderShape.Box) =>
                    BoxBox(collider1 as BoxCollider, collider2 as BoxCollider),
                (ColliderShape.Circle, ColliderShape.Box) => CircleBox(collider1 as CircleCollider,
                    collider2 as BoxCollider),
                (ColliderShape.Box, ColliderShape.Circle) => CircleBox(collider2 as CircleCollider,
                    collider1 as BoxCollider),
                _ => false,
            };
        }

        private static bool CircleCircle(CircleCollider circle1, CircleCollider circle2)
        {
            Vector2 center = circle2.Center - circle1.Center;
            float radius = circle2.Radius + circle1.Radius;
            return center.sqrMagnitude <= radius * radius;
        }

        private static bool BoxBox(BoxCollider collider1, BoxCollider collider2)
        {
            return collider1.AABB.Overlaps(collider2.AABB);
        }

        private static bool CircleBox(CircleCollider circle, BoxCollider box)
        {
            float dx = Mathf.Max(Mathf.Abs(circle.Center.x - box.Center.x) - box.HalfSize.x, 0);
            float dy = Mathf.Max(Mathf.Abs(circle.Center.y - box.Center.y) - box.HalfSize.y, 0);
            return dx * dx + dy * dy <= circle.Radius * circle.Radius;
        }
    }
}