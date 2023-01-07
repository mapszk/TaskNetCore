namespace TaskApp.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }
    }
}