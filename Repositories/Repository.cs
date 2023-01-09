using Microsoft.EntityFrameworkCore;
using TaskApp.Models;
using TaskApp.Repositories.Database;
using TaskApp.Repositories.Interfaces;

namespace TaskApp.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<T?> Get(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await context.Set<T>().AnyAsync(x => x.Id == id);
            return exists;
        }

    }
}