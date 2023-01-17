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

    public class UserInfoDTO
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public int ToDosAmmount { get; set; }
        public List<string>? UserRoles { get; set; }
    }

    public class AssignRoleDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }
    }

    public class ConfirmEmailDTO
    {
        [Required(ErrorMessage = "Token is required")]
        public string? Token { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
    }
}