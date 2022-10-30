namespace Autopark.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetList();
        Task Create(T item);
        Task Update(T item);
        Task Delete(int id);
        Task<T> Get(int id);

    }
}
