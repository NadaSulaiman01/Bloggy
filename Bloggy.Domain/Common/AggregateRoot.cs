namespace Bloggy.Domain.Common
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>
    {
        public virtual byte[]? RowVersion { get; protected set; }
        protected AggregateRoot() { }
        protected AggregateRoot(TKey id) : base(id) { }
    }
}
