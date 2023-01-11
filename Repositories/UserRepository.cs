using Microsoft.AspNetCore.Identity;
using TaskApp.Models;
using TaskApp.Repositories.Database;
using TaskApp.Repositories.Interfaces;

namespace TaskApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<IdentityResult> CreateUser(User user, string password)
        {
            return await this._userManager.CreateAsync(user, password);
        }
    }
}