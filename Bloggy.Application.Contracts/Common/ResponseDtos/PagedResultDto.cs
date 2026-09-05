namespace Bloggy.Application.Contracts.Common.ResponseDtos
{
    public class PagedResultDto<T> : ListResultDto<T>
    {
        public long TotalCount { get; set; }  
    }
}
