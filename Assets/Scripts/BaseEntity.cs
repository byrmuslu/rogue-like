using System;
using UnityEngine;

namespace Base.Manager
{
    public class BaseEntity : IEntity
    {
        public virtual int ID { get; set; }
        public string Name { get; set; }
        private int _maxHealth;

        public int MaxHealth
        {
            get => _maxHealth;
            set
            {
                _maxHealth = value;
                MaxHealthChanged?.Invoke(value);
            }
        }

        private int _health;

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                HealthChanged?.Invoke(value);
            }
        }

        private Vector2 _position;

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                PositionChanged?.Invoke(_position);
            }
        }

        private Quaternion _rotation;

        public Quaternion Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                RotationChanged?.Invoke(_rotation);
            }
        }

        public ICollider Collider { get; set; }
        public bool IsDestroyed { get; set; }
        public event Action<int> MaxHealthChanged;
        public event Action<int> HealthChanged;
        public event Action<Vector2> PositionChanged;
        public event Action<Quaternion> RotationChanged;
        public event Action<BaseEntity> Destroyed;

        public BaseEntity(string name, Vector2 position, Quaternion rotation)
        {
            Name = name;
            Position = position;
            Rotation = rotation;
        }

        public virtual void Update(float dt)
        {
            Collider.Center = Position;
        }

        public void Destroy()
        {
            IsDestroyed = true;
            Destroyed?.Invoke(this);
        }

        public void TakeHealth(int health)
        {
            Health = health;
            if (Health > _maxHealth)
            {
                Health = _maxHealth;
            }
        }
    }
}