using AutoMapper;
using Bloggy.Application.Contracts.Blogs;
using Bloggy.Application.Contracts.Blogs.RequestDtos;
using Bloggy.Application.Contracts.Blogs.ResponseDtos;
using Bloggy.Application.Contracts.Common;
using Bloggy.Domain;
using Bloggy.Domain.BlogAggregate;

namespace Bloggy.Application.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog, Guid> _repository;
        private readonly IMapper _mapper;

        public BlogService(IRepository<Blog, Guid> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateBlogAsync(CreateUpdateBlogRequestDto input, CancellationToken ct = default)
        {
            var blog = new Blog(input.Title, input.Content, Guid.Empty);
            await _repository.AddAsync(blog, ct);
            await _repository.SaveChangesAsync(ct);
        }
        public async Task DeleteBlogAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _repository.GetByIdAsync(id, ct);
            if (entity is null) throw new KeyNotFoundException($"Blog with id '{id}' was not found.");
            await _repository.DeleteAsync(entity, ct);
            await _repository.SaveChangesAsync(ct);
        }

        public async Task<PagedResultDto<BlogDto>> GetBlogsAsync(PagedResultRequestDto input, CancellationToken ct = default)
        {
            var pageSize = input.MaxResultCount > 0 ? input.MaxResultCount : 20;
            var pageIndex = (input.SkipCount / pageSize) + 1;
            var (items, total) = await _repository.GetAllReadOnlyAsync(pageIndex, pageSize, ct);
            var dto = new PagedResultDto<BlogDto>
            {
                Items = items.Select(x => _mapper.Map<BlogDto>(x)).ToList()
            };
            dto.TotalCount = total;
            return dto;
        }

        public async Task UpdateBlogAsync(Guid id, CreateUpdateBlogRequestDto input, CancellationToken ct = default)
        {
            var entity = await _repository.GetByIdAsync(id, ct);
            if (entity is null) throw new KeyNotFoundException($"Blog with id '{id}' was not found.");
            entity.Update(input.Title, input.Content);
            await _repository.UpdateAsync(entity, ct);
            await _repository.SaveChangesAsync(ct);
        }
    }
}
