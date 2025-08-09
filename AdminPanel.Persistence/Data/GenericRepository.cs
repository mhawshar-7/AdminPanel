
using AdminPanel.Data.Entities;
using AdminPanel.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Persistence.Data
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _context;

		public GenericRepository(StoreContext context)
		{
			_context = context;
		}

		public void Create(T entity)
		{
			_context.Set<T>().Add(entity);
		}

		public void Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await _context.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
		}
        public async Task<IReadOnlyList<T>> ListDeletedAsync()
        {
            return await _context.Set<T>().Where(x => x.IsDeleted).ToListAsync();
        }

        public void Update(T entity)
		{
			_context.Set<T>().Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}
	}
}
