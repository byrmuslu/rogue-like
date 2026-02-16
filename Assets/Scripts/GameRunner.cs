using System.Collections.Generic;
using UnityEngine;

namespace Base.Manager
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private List<EntityViewInfo> _entityViewInfos;
        private World _world;

        private void Awake()
        {
            _world = new World();
            _world.EntityAdded += OnEntityAdded;
            Character player = new Character("Test", Vector2.zero, Quaternion.identity);
            BoxCollider boxCollider = new BoxCollider(player, Vector3.zero, Vector3.one * .5f);
            player.Controller = new PlayerController(5f);
            player.Collider = boxCollider;
            RandomScatterAbility randomScatterAbility = new RandomScatterAbility(player, _world, 2f);
            player.Abilities.Add(randomScatterAbility);
            Character enemy = new Character("Enemy", Vector2.up * 4, Quaternion.identity);
            boxCollider = new BoxCollider(enemy, Vector2.up * 4, Vector2.one * .5f);
            enemy.Collider = boxCollider;
            enemy.Controller = new ChaseController(player, 2f, 0f);
            enemy.ID = 2;
            
            _world.PrimaryPlayer = player;
            
            // >>> Dalgaları sıraya al <<<
            var wave1 = new WaveDefinition(
                startDelay: 1.0f,
                lines: new[]
                {
                    new WaveLineDef(count: 5, interval: 0.8f, spreadRadius: 6f, factory: EnemyFactories.BasicChaser(2.0f)),
                });

            var wave2 = new WaveDefinition(
                startDelay: 3.0f,
                lines: new[]
                {
                    new WaveLineDef(count: 8, interval: 0.5f, spreadRadius: 7.5f, factory: EnemyFactories.BasicChaser(2.2f)),
                    new WaveLineDef(count: 4, interval: 1.2f, spreadRadius: 10f, factory: EnemyFactories.BasicChaser(3.0f)), // hızlı tip
                });

            var wave3 = new WaveDefinition(
                startDelay: 4.0f,
                lines: new[]
                {
                    new WaveLineDef(count: 12, interval: 0.35f, spreadRadius: 9f, factory: EnemyFactories.BasicChaser(2.5f)),
                });

            _world.Waves.Enqueue(wave1);
            _world.Waves.Enqueue(wave2);
            _world.Waves.Enqueue(wave3);

            // (İstersen event dinle)
            _world.Waves.WaveStarted += i => Debug.Log($"Wave {i+1} started");
            _world.Waves.WaveCompleted += i => Debug.Log($"Wave {i+1} completed");
            
            
            _world.AddEntity(player);
            _world.AddEntity(enemy);
        }

        private void OnEntityAdded(IEntity entity)
        {
            EntityView entityView = Instantiate(_entityViewInfos.Find(e => e.id == entity.ID).entityView);
            entityView.Initialize(entity);
        }

        private void Update()
        {
            _world.Update(Time.deltaTime);
        }
    }
}