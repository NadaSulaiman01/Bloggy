namespace Bloggy.Application.Contracts.Common
{
    public class ListResultDto<T>
    {
        public IReadOnlyList<T> Items { get; set; } = new List<T>();
    }
}
