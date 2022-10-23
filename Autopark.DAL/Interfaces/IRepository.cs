namespace Autopark.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetList();
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        T Get(int id);

    }
}
