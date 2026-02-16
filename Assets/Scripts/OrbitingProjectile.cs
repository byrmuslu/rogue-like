using UnityEngine;

namespace Base.Manager
{
    public class OrbitingProjectile : BaseEntity
    {
        private readonly IEntity _owner;
        private readonly float _radius;
        private readonly float _angularSpeed;
        private float _currentAngle;
        private float _timeToLive;

        public OrbitingProjectile(IEntity owner, Vector2 position, Quaternion rotation, float ttl, float initialAngle,
            float radius, float angSpeed) : base("OrbitingProjectile", position, rotation)
        {
            _owner = owner;
            _timeToLive = ttl;
            _currentAngle = initialAngle;
            _radius = radius;
            _angularSpeed = angSpeed;
        }

        public override void Update(float dt)
        {
            _timeToLive -= dt;
            if (_timeToLive <= 0f)
            {
                Destroy();
                return;
            }

            _currentAngle += _angularSpeed * Mathf.Deg2Rad * dt;
            if (_currentAngle >= 2 * Mathf.PI) _currentAngle -= 2 * Mathf.PI;
            Vector2 center = _owner.Position;
            Vector2 newPos = center + new Vector2(Mathf.Cos(_currentAngle), Mathf.Sin(_currentAngle)) * _radius;
            Position = newPos;
        }
    }
}