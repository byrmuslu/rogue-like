namespace Base.Manager
{
    public interface ICollidable
    {
        ICollider Collider { get; }
        void OnCollide(IEntity other);
    }
}