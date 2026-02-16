using System;
using UnityEngine;

namespace Base.Manager
{
    public static class EnemyFactories
    {
        // Basit "player'ı kovalayan" düşman
        public static Func<World, IEntity, Vector2, IEntity> BasicChaser(
            float moveSpeed = 2f,
            float stopDistance = 0f,
            float colliderHalfSize = 0.5f)
        {
            return (world, target, pos) =>
            {
                var enemy = new Character("Enemy", pos, Quaternion.identity);
                enemy.ID = 2;
                enemy.Collider = new BoxCollider(enemy, pos, Vector2.one * colliderHalfSize);
                enemy.Controller = new ChaseController(target, moveSpeed, stopDistance);
                return enemy;
            };
        }

        // İstersen farklı tipler: hızlı ama canı az, patlayan, menzilli vs. ekleyebilirsin.
    }
}