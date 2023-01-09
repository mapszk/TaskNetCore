using System.ComponentModel.DataAnnotations;

namespace TaskApp.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ToDoId { get; set; }
    }

    public class CreateCommentDTO
    {
        [Required]
        public string? Content { get; set; }
    }
}