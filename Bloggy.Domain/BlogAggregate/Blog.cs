using Bloggy.Domain.Common;

namespace Bloggy.Domain.BlogAggregate
{
    public class Blog : AggregateRoot<Guid>
    {
        public string Title { get; protected set; } = default!;
        public string Content { get; protected set; } = default!;
        public DateTime PublishedDate { get; protected set; }
        public Guid AuthorId { get; protected set; }
        protected Blog() { }
        public Blog(string title, string content, Guid authorId) : base(Guid.NewGuid())
        {
            Title = title;
            Content = content;
            PublishedDate = DateTime.Now;
            AuthorId = authorId;
        }

        public void Update(string title, string content)
        {
            Title = title;
            Content = content;
        }

    }
}
