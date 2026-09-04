using Bloggy.Domain.BlogAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloggy.EntityFrameworkCore
{
    public class BloggyDbContext : DbContext
    {
        public DbSet<Blog> Blogs => Set<Blog>();
        public BloggyDbContext(
        DbContextOptions<BloggyDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(
                typeof(BloggyDbContext).Assembly);
        }
    }
}
