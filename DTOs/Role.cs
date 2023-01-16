using System.ComponentModel.DataAnnotations;

namespace TaskApp.DTOs
{
    public class CreateRoleDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
    }
}