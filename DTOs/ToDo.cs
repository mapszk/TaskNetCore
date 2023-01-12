using System.ComponentModel.DataAnnotations;

namespace TaskApp.DTOs
{
    public class ToDoDTO
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CommentDTO>? Comments { get; set; }
    }

    public class ToDoShortDTO
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CommentsAmount { get; set; }
    }

    public class CreateToDoDTO
    {
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
    }

    public class UpdateToDoDTO
    {
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Completed status is required")]
        public bool Completed { get; set; }
    }
}