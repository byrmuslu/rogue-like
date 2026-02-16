using UnityEngine;

namespace Base.Manager
{
    public class EntityView : MonoBehaviour
    {
        private IEntity _entity;
        
        public void Initialize(IEntity entity)
        {
            _entity = entity;
            
            transform.position = entity.Position;
            transform.rotation = entity.Rotation;
            
            entity.Destroyed += OnDestroyed;
            entity.HealthChanged += OnHealthChanged;
            entity.MaxHealthChanged += OnMaxHealthChanged;
            entity.PositionChanged += OnPositionChanged;
            entity.RotationChanged += OnRotationChanged;
        }

        private void OnHealthChanged(int health)
        {
        }

        private void OnMaxHealthChanged(int maxHealth)
        {
        }

        private void OnPositionChanged(Vector2 position)
        {
            transform.position = position;
        }

        private void OnRotationChanged(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        private void OnDestroyed(BaseEntity _)
        {
            Destroy();
        }

        private void Destroy()
        {
            _entity = null;
            
            Destroy(gameObject);
        }
    }
}