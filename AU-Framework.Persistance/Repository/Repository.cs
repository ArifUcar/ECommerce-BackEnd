using AU_Framework.Application.Repository;
using AU_Framework.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using AU_Framework.Domain.Abstract;

namespace AU_Framework.Persistance.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public DbContext Context => _context;

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Geçersiz ID formatı!", nameof(id));

            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }


        public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<T?> GetFirstWithIncludeAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> include,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;
            query = include(query);
            return await query.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public Task<IQueryable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dbSet.AsQueryable());
        }

        public Task<IQueryable<T>> GetAllWithIncludeAsync(
            Func<IQueryable<T>, IQueryable<T>> include,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;
            query = include(query);
            return Task.FromResult(query);
        }

        public Task<IQueryable<T>> GetAllWithPredicateAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dbSet.Where(predicate));
        }

        public Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dbSet.Where(predicate));
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            var entry = await _dbSet.AddAsync(entity, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            var entry = _dbSet.Update(entity);
            await SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }

        public async Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            var entry = _dbSet.Remove(entity);
            await SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _dbSet.RemoveRange(entities);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
