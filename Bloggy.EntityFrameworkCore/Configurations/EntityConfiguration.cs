using Bloggy.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bloggy.EntityFrameworkCore.Configurations
{
    public abstract class EntityConfiguration<TEntity, TKey>
        : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreationTime)
                .IsRequired();

            builder.Property(x => x.CreatorId)
                .IsRequired(false);
        }
    }
}
