using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskApp.Models;

namespace TaskApp.Repositories.Database
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.ToDoRepository = new ToDoRepository(this.context);
            this.CommentRepository = new CommentRepository(this.context);
            this.UserRepository = new UserRepository(this.context, userManager);
        }
        public ToDoRepository ToDoRepository { get; private set; }
        public CommentRepository CommentRepository { get; private set; }
        public UserRepository UserRepository { get; private set; }

        public async Task SaveAsync()
        {
            AddTimestamps();
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        private void AddTimestamps()
        {
            var entities = context.ChangeTracker
                .Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = DateTime.UtcNow;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}