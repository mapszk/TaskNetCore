using Microsoft.AspNetCore.Identity;
using TaskApp.Models;

namespace TaskApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUser(User user, string password);
        Task<User?> FindByEmailOrUsername(string emailOrUsername);
        Task<bool> ExistsByEmailOrUsername(string emailOrUsername);
        Task<IdentityResult> CreateRole(Role role);
        Task<Role> FindRole(string name);
        Task<List<Role>> GetRoles();
        Task<bool> RoleExists(string name);
        Task<IdentityResult> AssignRoleToUser(User user, string role);
        Task<IdentityResult> RemoveRoleToUser(User user, string role);
    }
}