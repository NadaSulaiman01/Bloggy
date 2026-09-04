using Bloggy.Domain;
using Bloggy.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bloggy.EntityFrameworkCore.Configurations
{
    public abstract class AggregateRootConfiguration<TEntity, TKey>
    : EntityConfiguration<TEntity, TKey>
    where TEntity : AggregateRoot<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.ConcurrencyStamp)
                .IsRequired()
                .HasMaxLength(StringLengths.ConcurrencyStampLength);
        }
    }
}
