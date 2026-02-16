using UnityEngine;

namespace Base.Manager
{
    public class ChaseController : IController
    {
        private readonly IEntity _target;
        private readonly float _moveSpeed;
        private readonly float _stopDistance;

        public ChaseController(IEntity target, float moveSpeed, float stopDistance)
        {
            _target = target;
            _moveSpeed = moveSpeed;
            _stopDistance = stopDistance;
        }

        public void Update(Character character, float dt)
        {
            if (_target == null || _target.IsDestroyed)
            {
                return;
            }

            Vector2 selfPos = character.Position;
            Vector2 targetPos = _target.Position;
            float sqrDistance = (targetPos - selfPos).sqrMagnitude;
            if (_stopDistance > 0f && sqrDistance <= _stopDistance * _stopDistance)
            {
                return;
            }

            Vector2 direction = (targetPos - selfPos).normalized;
            character.Position = selfPos + direction * _moveSpeed * dt;
        }
    }
}