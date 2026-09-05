namespace Bloggy.Application.Contracts.Common
{
    public abstract class EntityDto<TKey>
    {
        public TKey Id { get; set; } = default!;
    }
}
