namespace TaskApp.Repositories.Database
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            this.ToDoRepository = new ToDoRepository(this.context);
            this.CommentRepository = new CommentRepository(this.context);
        }
        public ToDoRepository ToDoRepository { get; private set; }
        public CommentRepository CommentRepository { get; private set; }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}