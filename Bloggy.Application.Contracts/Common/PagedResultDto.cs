namespace Bloggy.Application.Contracts.Common
{
    public class PagedResultDto<T> : ListResultDto<T>
    {
        public long TotalCount { get; set; }  
    }
}
