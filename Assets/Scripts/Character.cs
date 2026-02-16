using System.Collections.Generic;
using UnityEngine;

namespace Base.Manager
{
    public class Character : BaseEntity, IDamageable
    {
        public List<IAbility> Abilities { get; set; }
        public IController Controller { get; set; }

        public Character(string name, Vector2 position, Quaternion rotation) : base(name, position, rotation)
        {
            Abilities = new List<IAbility>();
            Controller = new IdleController();
        }

        public override void Update(float dt)
        {
            Controller?.Update(this, dt);
            Abilities.ForEach(ability =>
            {
                ability.Update(dt);
                if (ability.CanUse)
                {
                    ability.Use();
                }
            });
            base.Update(dt);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Destroy();
            }
        }
    }
}