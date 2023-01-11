using Microsoft.AspNetCore.Identity;

namespace TaskApp.Models
{
    public class User : IdentityUser
    {
        public string? Username { get; set; }
    }
}