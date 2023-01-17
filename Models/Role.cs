using Microsoft.AspNetCore.Identity;

namespace TaskApp.Models
{
    public class Role : IdentityRole
    {
        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}