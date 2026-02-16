namespace Base.Manager
{
    public abstract class Ability : IAbility
    {
        public abstract string Name { get; }
        public IEntity Owner { get; set; }
        public float Cooldown { get; set; }
        private float _cooldownTimer; 
        public bool CanUse => _cooldownTimer <= 0;

        public Ability(IEntity owner, float cooldown)
        {
            Cooldown = cooldown;
        }

        public void Use()
        {
            if (!CanUse)
            {
                return;
            } 
            _cooldownTimer = Cooldown;
            OnUse();
        } 
        
        protected abstract void OnUse();

        public virtual void Update(float dt)
        {
            if (_cooldownTimer > 0f)
            {
                _cooldownTimer -= dt;
            }
        }
    }
}