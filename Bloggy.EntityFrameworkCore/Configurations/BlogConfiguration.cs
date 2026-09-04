using Bloggy.Domain.BlogAggregate;
using Bloggy.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bloggy.EntityFrameworkCore.Configurations
{
    public class BlogConfiguration : AggregateRootConfiguration<Blog, Guid>
    {
        public override void Configure(EntityTypeBuilder<Blog> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(Blog));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(StringLengths.ShortTitleLength);

            builder.HasIndex(x => x.Title)
                .IsUnique();

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(StringLengths.LongContentLength);

            builder.Property(x => x.AuthorId)
                .IsRequired();

            builder.Property(x => x.PublishedDate)
                .IsRequired();
        }
    }
}
