namespace Bloggy.Application.Contracts.Common.ResponseDtos
{
    public abstract class EntityDto<TKey>
    {
        public TKey Id { get; set; } = default!;
    }
}
