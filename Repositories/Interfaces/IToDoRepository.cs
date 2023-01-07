using TaskApp.Models;

namespace TaskApp.Repositories.Interfaces
{
    public interface IToDoRepository : IDisposable
    {
        IEnumerable<ToDo> GetAll();
        ToDo Get(int id);
        void Add(ToDo toDo);
        void Update(ToDo toDo);
        void Remove(int id);
        void Save();
    }
}