using Microsoft.EntityFrameworkCore;
using TaskApp.Models;

namespace TaskApp.Repositories.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ToDo>? ToDos { get; set; }
    }
}