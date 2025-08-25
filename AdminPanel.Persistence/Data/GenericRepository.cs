
using AdminPanel.Data.Entities;
using AdminPanel.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Persistence.Data
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class, ISoftDeletable
	{
		private readonly StoreContext _context;

		public GenericRepository(StoreContext context)
		{
			_context = context;
		}

        public Task<int> Count()
        {
			return _context.Set<T>().CountAsync(x => !x.IsDeleted);
        }
        public Task<int> CountDeleted()
        {
            return _context.Set<T>().CountAsync(x => x.IsDeleted);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
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

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await _context.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
		}
        public async Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
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

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
