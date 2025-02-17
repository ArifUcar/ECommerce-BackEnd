#nullable enable
using System.Linq.Expressions;

namespace AU_Framework.Application.Repository
{
    public interface IRepository<T> where T : class
    {
        // Tekil kayıt getirme işlemleri
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<T?> GetFirstWithIncludeAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> include,
            CancellationToken cancellationToken = default);
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        // Çoklu kayıt getirme işlemleri
        Task<IQueryable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IQueryable<T>> GetAllWithIncludeAsync(
            Func<IQueryable<T>, IQueryable<T>> include,
            CancellationToken cancellationToken = default);
        Task<IQueryable<T>> GetAllWithPredicateAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default);
        Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        // Ekleme işlemleri
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        // Güncelleme işlemi
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        // Silme işlemleri
        Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        // Yardımcı metodlar
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
