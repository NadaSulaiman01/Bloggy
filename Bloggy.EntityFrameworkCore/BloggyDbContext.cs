using Bloggy.Domain.BlogAggregate;
using Bloggy.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.EntityFrameworkCore
{
    public class BloggyDbContext : DbContext
    {
        public DbSet<Blog> Blogs => Set<Blog>();
        private readonly Func<Guid?>? _getCurrentUserId;

        public BloggyDbContext(
            DbContextOptions<BloggyDbContext> options,
            Func<Guid?>? getCurrentUserId = null)
            : base(options)
        {
            _getCurrentUserId = getCurrentUserId;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(
                typeof(BloggyDbContext).Assembly);
        }

        public override int SaveChanges()
        {
            ApplyAuditing();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditing();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditing()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in entries)
            {
                // Set CreationTime if property exists
                var creationTimeProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == nameof(IAuditedObject.CreationTime));
                if (creationTimeProp != null)
                {
                    creationTimeProp.CurrentValue = DateTime.UtcNow;
                }

                // Set CreatorId if property exists and current user available
                var creatorIdProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == nameof(IAuditedObject.CreatorId));
                if (creatorIdProp != null && _getCurrentUserId != null)
                {
                    var id = _getCurrentUserId();
                    if (id != null)
                    {
                        creatorIdProp.CurrentValue = id;
                    }
                }
            }
        }
    }
}
