using System;
using UnityEngine;

namespace Base.Manager
{
    public interface IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public Vector2 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public ICollider Collider { get; set; }
        public bool IsDestroyed { get; set; }
        void Update(float dt);
        void Destroy();
        void TakeHealth(int health);
        public event Action<int> MaxHealthChanged;
        public event Action<int> HealthChanged;
        public event Action<Vector2> PositionChanged;
        public event Action<Quaternion> RotationChanged;
        public event Action<BaseEntity> Destroyed;
    }
}