using AutoMapper;
using Bloggy.Application.Contracts.Blogs;
using Bloggy.Application.Contracts.Blogs.RequestDtos;
using Bloggy.Application.Contracts.Blogs.ResponseDtos;
using Bloggy.Application.Contracts.Common;
using Bloggy.Application.Contracts.Common.ResponseDtos;
using Bloggy.Domain;
using Bloggy.Domain.BlogAggregate;
using Bloggy.Domain.Shared;

namespace Bloggy.Application.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog, Guid> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public BlogService(IRepository<Blog, Guid> repository,
            IMapper mapper,
            ICurrentUser currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task CreateBlogAsync(CreateUpdateBlogRequestDto input, CancellationToken ct = default)
        {
            var blogTitleExists  = await _repository.ExistsAsync(x => x.Title == input.Title, ct);

            if (blogTitleExists)
            {
                throw new Exception(ErrorCodes.DuplicateBlogTitle);
            }

            var blog = new Blog(input.Title, input.Content, Guid.NewGuid());
            await _repository.AddAsync(blog, ct);
            await _repository.SaveChangesAsync(ct);
        }
        public async Task DeleteBlogAsync(Guid id, CancellationToken ct = default)
        {
            var blog = await _repository.GetByIdAsync(id, ct);

            if (blog is null)
            {
                throw new Exception(ErrorCodes.BlogNotFound);
            }
            await _repository.DeleteAsync(blog, ct);
            await _repository.SaveChangesAsync(ct);
        }

        public async Task<PagedResultDto<BlogDto>> GetBlogsAsync(PagedResultRequestDto input, CancellationToken ct = default)
        {
            var pageSize = input.MaxResultCount > 0 ? input.MaxResultCount : 20;
            var pageIndex = (input.SkipCount / pageSize) + 1;
            var (items, total) = await _repository.GetAllReadOnlyAsync(pageIndex, pageSize, null,ct);
            var dto = new PagedResultDto<BlogDto>
            {
                Items = items.Select(x => _mapper.Map<BlogDto>(x)).ToList()
            };
            dto.TotalCount = total;
            return dto;
        }

        public async Task<PagedResultDto<BlogDto>> GetCurrentUserBlogsAsync(PagedResultRequestDto input, CancellationToken ct = default)
        {
            var pageSize = input.MaxResultCount > 0 ? input.MaxResultCount : 20;
            var pageIndex = (input.SkipCount / pageSize) + 1;

            var (items, total) = await _repository.GetAllReadOnlyAsync(pageIndex, pageSize, x => x.AuthorId == _currentUser.Id,ct);
            var dto = new PagedResultDto<BlogDto>
            {
                Items = items.Select(x => _mapper.Map<BlogDto>(x)).ToList()
            };
            dto.TotalCount = total;
            return dto;
        }

        public async Task UpdateBlogAsync(Guid id, CreateUpdateBlogRequestDto input, CancellationToken ct = default)
        {
            var blog = await _repository.GetByIdAsync(id, ct);

            if (blog is null)
            {
                throw new Exception(ErrorCodes.BlogNotFound);
            }

            blog.Update(input.Title, input.Content);
            await _repository.UpdateAsync(blog, ct);
            await _repository.SaveChangesAsync(ct);
        }
    }
}
