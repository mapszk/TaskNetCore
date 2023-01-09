using Microsoft.EntityFrameworkCore;
using TaskApp.Models;
using TaskApp.Repositories.Database;
using TaskApp.Repositories.Interfaces;

namespace TaskApp.Repositories
{
    public class ToDoRepository : Repository<ToDo>, IToDoRepository
    {
        public ToDoRepository(ApplicationDbContext context) : base(context) { }

        public ToDo GetToDoDetails(int id)
        {
            return context.ToDos.Where(toDo => toDo.Id == id)
                .Include(toDo => toDo.Comments)
                .Single();
        }

        public async Task<List<ToDo>> GetShortToDos()
        {
            return await context.ToDos.Include(toDo => toDo.Comments).ToListAsync();
        }
    }
}