using AdminPanel.Data.Interfaces;

namespace AdminPanel.Persistence.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListDeletedAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> Count();
        Task<int> CountDeleted();
    }
}