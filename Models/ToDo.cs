namespace TaskApp.Models
{
    public class ToDo : BaseEntity
    {
        public new int Id { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }
    }
}