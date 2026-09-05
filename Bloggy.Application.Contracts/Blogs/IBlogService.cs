using Bloggy.Application.Contracts.Blogs.RequestDtos;
using Bloggy.Application.Contracts.Blogs.ResponseDtos;
using Bloggy.Application.Contracts.Common.ResponseDtos;

namespace Bloggy.Application.Contracts.Blogs
{
    public interface IBlogService
    {
        Task CreateBlogAsync(CreateUpdateBlogRequestDto input, CancellationToken ct = default);
        Task UpdateBlogAsync(Guid id, CreateUpdateBlogRequestDto input, CancellationToken ct = default);
        Task<PagedResultDto<BlogDto>> GetBlogsAsync(PagedResultRequestDto input, CancellationToken ct = default);
        Task<PagedResultDto<BlogDto>> GetCurrentUserBlogsAsync(PagedResultRequestDto input, CancellationToken ct = default);
        Task DeleteBlogAsync(Guid id, CancellationToken ct = default);
    }
}
