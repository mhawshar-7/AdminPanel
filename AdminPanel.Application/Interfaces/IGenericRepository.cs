using AdminPanel.Data.Entities;

namespace AdminPanel.Data.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListDeletedAsync();
		void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> Count();
    }
}