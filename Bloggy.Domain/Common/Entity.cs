namespace Bloggy.Domain.Common
{
    public abstract class Entity<TKey>
    {
        public virtual TKey Id { get; protected set; } = default!;
        public virtual DateTime CreationTime { get; protected set; }
        public virtual Guid? CreatorId { get; protected set; }
        protected Entity() { }
        protected Entity(TKey id)
        {
            Id = id;
        }
    }
}
