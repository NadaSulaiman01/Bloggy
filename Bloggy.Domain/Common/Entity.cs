namespace Bloggy.Domain.Common
{
    public abstract class Entity<TKey> : IAuditedObject
    {
        public virtual TKey Id { get; protected set; } = default!;

        public DateTime CreationTime { get; protected set; }

        public Guid? CreatorId { get; protected set; }

        protected Entity() { }
        protected Entity(TKey id)
        {
            Id = id;
        }
    }
}
