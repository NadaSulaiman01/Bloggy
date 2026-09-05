namespace Bloggy.Application.Contracts.Common.ResponseDtos
{
    public class ListResultDto<T>
    {
        public IReadOnlyList<T> Items { get; set; } = new List<T>();
    }
}
