using Microsoft.EntityFrameworkCore;
using TaskApp.Models;
using TaskApp.Repositories.Database;
using TaskApp.Repositories.Interfaces;

namespace TaskApp.Repositories
{
    public class ToDoRepository : Repository<ToDo>, IToDoRepository
    {
        public ToDoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<ToDo> GetToDoDetails(int id)
        {
            return await context.ToDos.Include(toDo => toDo.Comments).FirstOrDefaultAsync(toDo => toDo.Id == id);
        }

        public async Task<List<ToDo>> GetShortToDos()
        {
            return await context.ToDos.Include(toDo => toDo.Comments).ToListAsync();
        }

        public async Task<(List<ToDo>, int)> GetAllPaginated(string description, int pageSize, int pageNumber)
        {
            var result = await context.ToDos
                .Where(t => String.IsNullOrEmpty(description) || t.Description.ToLower().Contains(description.ToLower()))
                .Include(t => t.Comments)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (result, result.Count());
        }
    }
}