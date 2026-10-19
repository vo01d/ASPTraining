using ProductCatalog.Core.Entities.Abstractions;

namespace ProductCatalog.Application.Abstractions.Repositories {
    public interface IRepository<T>
        where T : Entity {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);

        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);

        void Update(T entity);

        void Delete(T entity);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
