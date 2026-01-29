namespace Playhouse.Core.Services.EntityManagerService.Abstractions
{
	public interface IEntityManager<T> where T : class
	{
		Task<IList<T>> GetAsync();
		Task<T?> GetAsync(int id);
		Task CreateOrUpdate(T obj);
		void Delete(int id);
	}
}
