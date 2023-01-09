using System.ComponentModel.DataAnnotations;

namespace TaskApp.Models
{
    public class Comment : BaseEntity
    {
        [Required]
        public string? Content { get; set; }
        [Required]
        public int ToDoId { get; set; }
        [Required]
        public ToDo? Todo { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}