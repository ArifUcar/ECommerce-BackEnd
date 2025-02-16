using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace AU_Framework.Application.Repository
{
    public interface IRepository<T> where T : class
    {
        // Tekil kayıt getirme işlemleri
        Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        // Çoklu kayıt getirme işlemleri
        Task<IQueryable<T>> GetAllAsync(CancellationToken cancellationToken = default);
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
