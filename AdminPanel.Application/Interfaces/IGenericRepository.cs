using AdminPanel.Data.Entities;
using AdminPanel.Data.Entities.Identity;

namespace AdminPanel.Data.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListDeletedAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> Count();
    }
}