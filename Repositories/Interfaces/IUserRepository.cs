using Microsoft.AspNetCore.Identity;
using TaskApp.Models;

namespace TaskApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUser(User user, string password);
        Task<User?> FindByEmail(string email);
        bool ExistsByEmailOrUsername(string email);
    }
}