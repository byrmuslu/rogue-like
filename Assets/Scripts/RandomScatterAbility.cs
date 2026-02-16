using UnityEngine;

namespace Base.Manager
{
    public class RandomScatterAbility : Ability
    {
        public override string Name => nameof(RandomScatterAbility);
        private int _count;
        private float _speed;
        private IEntity _owner;
        private World _world;

        public RandomScatterAbility(IEntity owner, World world, float cooldown, int count = 4, float speed = 1) : base(
            owner, cooldown)
        {
            _owner = owner;
            _world = world;
            _count = count;
            _speed = speed;
        }

        protected override void OnUse()
        {
            Vector2 origin = _owner.Position;
            for (int i = 0; i < _count; i++)
            {
                Vector2 direction = UnityEngine.Random.insideUnitCircle.normalized;
                if (direction == Vector2.zero)
                {
                    direction = Vector2.right;
                }

                Projectile projectile = new Projectile(origin, Quaternion.identity, 2, _owner, direction * _speed);
                CircleCollider circleCollider = new CircleCollider(projectile, origin, .3f);
                projectile.Collider = circleCollider;
                _world.AddEntity(projectile);
            }
        }
    }
}