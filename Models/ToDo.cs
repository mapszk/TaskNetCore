using System.ComponentModel.DataAnnotations;

namespace TaskApp.Models
{
    public class ToDo : BaseEntity
    {
        [Required]
        public string? Description { get; set; }
        [Required]
        public bool Completed { get; set; }
        public List<Comment>? Comments { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public User? User { get; set; }
    }
}