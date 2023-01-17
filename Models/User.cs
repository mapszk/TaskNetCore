using Microsoft.AspNetCore.Identity;

namespace TaskApp.Models
{
    public class User : IdentityUser
    {
        public List<ToDo>? ToDos { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}