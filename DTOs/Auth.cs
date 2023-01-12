using System.ComponentModel.DataAnnotations;

namespace TaskApp.DTOs
{
    public class UserRegistrationDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; init; }
    }

    public class UserSignInDTO
    {
        [Required(ErrorMessage = "Email or username is required")]
        public string? EmailOrUsername { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
    }
}