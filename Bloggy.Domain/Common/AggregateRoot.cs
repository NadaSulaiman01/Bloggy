namespace Bloggy.Domain.Common
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>
    {
        public virtual string ConcurrencyStamp { get; protected set; }
        protected AggregateRoot() { }
        protected AggregateRoot(TKey id) : base(id) { }
    }
}
