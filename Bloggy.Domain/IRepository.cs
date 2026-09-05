using System.Linq.Expressions;

namespace Bloggy.Domain
{
    public interface IRepository<T, TKey>
        where T : class
    {
        Task<T?> GetByIdAsync(TKey id, CancellationToken ct = default);
        Task<(List<T> Items, long TotalCount)> GetAllAsync(int pageIndex = 1, int pageSize = 20, CancellationToken ct = default);
        Task<(IReadOnlyList<T> Items, long TotalCount)> GetAllReadOnlyAsync(int pageIndex = 1, int pageSize = 20, CancellationToken ct = default);
        Task AddAsync(T entity, CancellationToken ct = default);
        Task UpdateAsync(T entity, CancellationToken ct = default);
        Task DeleteAsync(T entity, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
    }
}
