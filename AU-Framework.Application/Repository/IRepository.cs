using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;

namespace AU_Framework.Application.Repository
{
    public interface IRepository<T> where T : class
    {
        // Tekil kayıt getirme
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        // Tüm kayıtları getirme
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        // Şarta göre kayıtları getirme
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        // Tekil kayıt ekleme
        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        // Çoklu kayıt ekleme
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        // Kayıt güncelleme
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        // Tekil kayıt silme
        Task RemoveAsync(T entity, CancellationToken cancellationToken = default);

        // Çoklu kayıt silme
        Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        // Şarta göre tek kayıt getirme
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        // Kayıt var mı kontrolü
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
