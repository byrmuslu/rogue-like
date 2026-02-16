namespace Base.Manager
{
    public interface IAbility
    {
        public string Name { get; }
        public IEntity Owner { get; set; }
        public float Cooldown { get; set; }
        public bool CanUse { get; }
        void Use();
        void Update(float dt);
    }
}