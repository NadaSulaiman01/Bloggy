using Bloggy.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.EntityFrameworkCore.Repositories
{
    public class EfRepository<T, TKey> : IRepository<T, TKey>
        where T : class
    {
        private readonly BloggyDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EfRepository(BloggyDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
        }

        public async Task DeleteAsync(T entity, CancellationToken ct = default)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }


        public async Task<(List<T> Items, long TotalCount)> GetAllAsync(int pageIndex = 1, int pageSize = 20, CancellationToken ct = default)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 20;

            var query = _dbSet.AsQueryable();
            var total = await query.LongCountAsync(ct);
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(ct);
            return (items, total);
        }
        public async Task<(IReadOnlyList<T> Items, long TotalCount)> GetAllReadOnlyAsync(int pageIndex = 1, int pageSize = 20, CancellationToken ct = default)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 20;

            var query = _dbSet.AsNoTracking();

            var total = await query.LongCountAsync(ct);

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task<T?> GetByIdAsync(TKey id, CancellationToken ct = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, ct);
        }

        public async Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
