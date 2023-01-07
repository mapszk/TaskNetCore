using System.ComponentModel.DataAnnotations;

namespace TaskApp.DTOs
{
    public class ToDoDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }
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