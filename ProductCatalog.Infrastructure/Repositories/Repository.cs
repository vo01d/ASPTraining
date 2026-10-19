using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Abstractions.Repositories;
using ProductCatalog.Core.Entities.Abstractions;
using ProductCatalog.Infrastructure.Context;

namespace ProductCatalog.Infrastructure.Repositories {
    public class Repository<T> : IRepository<T> where T : Entity {
        protected readonly ProductCatalogDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ProductCatalogDbContext context) {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken) {
            return (await _dbSet.AddAsync(entity, cancellationToken)).Entity;
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken) {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken) {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public void Update(T entity) {
            _dbSet.Update(entity);
        }

        public void Delete(T entity) {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken) {
            await _context.SaveChangesAsync(cancellationToken); 
        }
    }
}
