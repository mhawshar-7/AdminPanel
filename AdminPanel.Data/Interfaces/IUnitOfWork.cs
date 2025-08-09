using AdminPanel.Data.Entities;

namespace AdminPanel.Data.Interfaces
{
	public interface IUnitOfWork
	{
		IGenericRepository<TEntity> Repository<TEntity>() where TEntity: BaseEntity;
		Task<int> Complete();
	}
}
