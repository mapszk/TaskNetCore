using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User?> FindByEmailOrUsername(string emailOrUsername)
        {
            return await _context.Users
                .Include(u => u.ToDos)
                .FirstOrDefaultAsync(u => u.Email == emailOrUsername || u.UserName == emailOrUsername);
        }

        public async Task<bool> ExistsByEmailOrUsername(string emailOrUsername)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == emailOrUsername || u.UserName == emailOrUsername) != null;
        }

        public async Task<IdentityResult> AddRoleToUser(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }
    }
}