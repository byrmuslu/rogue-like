using UnityEngine;

namespace Base.Manager
{
    public class OrbitalScatterAbility : Ability
    {
        public override string Name => nameof(OrbitalScatterAbility);
        private readonly IEntity _owner;
        private readonly World _world;
        private readonly int _count;
        private readonly float _orbitalRadius;
        private readonly float _angularSpeed;
        private readonly float _timeToLive;

        public OrbitalScatterAbility(IEntity owner, World world, int count = 6, float orbitRadius = 2f,
            float angularSpeed = 120f, float timeToLive = 4f, float cooldown = 2f) : base(owner, cooldown)
        {
            _owner = owner;
            _world = world;
            _count = count;
            _orbitalRadius = orbitRadius;
            _angularSpeed = angularSpeed;
            _timeToLive = timeToLive;
        }

        protected override void OnUse()
        {
            var center = _owner.Position;
            float angleStep = 360f / _count;
            for (int i = 0; i < _count; i++)
            {
                float angle = (i * angleStep) * Mathf.Deg2Rad;
                Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _orbitalRadius;
                var proj = new OrbitingProjectile(position: center + offset, rotation: Quaternion.identity,
                    ttl: _timeToLive, owner: _owner, initialAngle: angle, radius: _orbitalRadius,
                    angSpeed: _angularSpeed);
                var collider = new CircleCollider(proj, center + offset, .3f);
                proj.Collider = collider;
                _world.AddEntity(proj);
            }
        }
    }
}