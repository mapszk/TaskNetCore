namespace TaskApp.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> Get(int id);
        Task<List<T>> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<bool> Exists(int id);
    }
}