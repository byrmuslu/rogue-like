using UnityEngine;

namespace Base.Manager
{
    public class Projectile : BaseEntity, ICollidable
    {
        public override int ID { get; set; } = 1;
        public IEntity Owner { get; set; }
        public Vector2 Velocity { get; set; }
        private float _timeToLive;

        public Projectile(Vector2 position, Quaternion rotation, float timeToLive, IEntity owner, Vector2 velocity) :
            base("Projectile", position, rotation)
        {
            _timeToLive = timeToLive;
            Owner = owner;
            Velocity = velocity;
        }

        public override void Update(float dt)
        {
            Position += (Velocity * dt);
            _timeToLive -= dt;
            if (_timeToLive <= 0f)
            {
                Destroy();
            }

            base.Update(dt);
        }

        public void OnCollide(IEntity other)
        {
            if (other == Owner || other is not IDamageable damageable)
            {
                return;
            }

            damageable.TakeDamage(1);
        }
    }
}