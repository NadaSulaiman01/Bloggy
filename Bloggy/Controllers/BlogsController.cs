using Bloggy.Application.Contracts.Blogs;
using Bloggy.Application.Contracts.Blogs.RequestDtos;
using Bloggy.Application.Contracts.Blogs.ResponseDtos;
using Bloggy.Application.Contracts.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggy.HttpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase, IBlogService
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [Authorize]
        [HttpPost]
        public Task CreateBlogAsync([FromBody] CreateUpdateBlogRequestDto input, CancellationToken ct = default)
        => _blogService.CreateBlogAsync(input, ct);

        [Authorize]
        [HttpPut("{id}")]
        public Task UpdateBlogAsync(Guid id, [FromBody] CreateUpdateBlogRequestDto input, CancellationToken ct = default)
            => _blogService.UpdateBlogAsync(id, input, ct);

        [HttpGet]
        public Task<PagedResultDto<BlogDto>> GetBlogsAsync([FromQuery] PagedResultRequestDto input, CancellationToken ct = default)
            => _blogService.GetBlogsAsync(input, ct);

        [Authorize]
        [HttpDelete("{id}")]
        public Task DeleteBlogAsync(Guid id, CancellationToken ct = default)
            => _blogService.DeleteBlogAsync(id, ct);

    }
}
