using Microsoft.AspNetCore.Identity;
using TaskApp.Models;

namespace TaskApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUser(User user, string password);
    }
}