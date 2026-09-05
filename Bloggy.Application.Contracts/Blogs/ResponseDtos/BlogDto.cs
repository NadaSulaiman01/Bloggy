using Bloggy.Application.Contracts.Common;

namespace Bloggy.Application.Contracts.Blogs.ResponseDtos
{
    public class BlogDto : EntityDto<Guid>
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime PublishedDate { get; set; }
    }
}
