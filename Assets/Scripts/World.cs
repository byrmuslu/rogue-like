using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.Manager
{
    public class World
    {
        public string Name { get; set; }
        public List<IEntity> Entities { get; private set; }

        private List<ICollidable> _collidables;

        // >>> YENİ: Oyuncu referansı ve Waves <<<
        public IEntity PrimaryPlayer { get; set; }
        public WaveManager Waves { get; private set; }

        public event Action<IEntity> EntityAdded; 

        public World()
        {
            Entities = new List<IEntity>();
            _collidables = new List<ICollidable>();
            Waves = new WaveManager(this);
        }

        public void Update(float dt)
        {
            // >>> YENİ: Önce dalga/spawn zamanlaması <<<
            Waves.Update(dt);

            // Mevcut entity update akışı
            List<IEntity> entities = Entities.ToList();
            entities.ForEach(e => { e.Update(dt); });

            _collidables.ToList().ForEach(c =>
            {
                if (!c.Collider.Owner.IsDestroyed)
                {
                    entities.ForEach(e =>
                    {
                        if (!e.IsDestroyed && c.Collider != e.Collider)
                        {
                            if (ColliderOverlaps.Overlaps(c.Collider, e.Collider))
                            {
                                c.OnCollide(e);
                            }
                        }
                    });
                }
            });
        }

        public void AddEntity(IEntity entity)
        {
            if (entity is ICollidable collidable)
            {
                _collidables.Add(collidable);
            }

            entity.Destroyed += OnEntityDestroyed;
            Entities.Add(entity);
            EntityAdded?.Invoke(entity);
        }

        private void OnEntityDestroyed(BaseEntity entity)
        {
            entity.Destroyed -= OnEntityDestroyed;

            if (entity is ICollidable collidable)
            {
                _collidables.Remove(collidable);
            }
            Entities.Remove(entity);
        }

        public void RemoveEntity(IEntity entity)
        {
            Entities.Remove(entity);
        }
    }
}