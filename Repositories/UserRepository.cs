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
        private readonly RoleManager<Role> _roleManager;

        public UserRepository(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateUser(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User?> FindByEmailOrUsername(string emailOrUsername)
        {
            return await _context.Users
                .Include(u => u.ToDos)
                .Include(u => u.UserRoles)
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

        public async Task<IdentityResult> CreateRole(Role role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<Role> FindRole(string name)
        {
            return await _roleManager.FindByNameAsync(name);
        }

        public async Task<List<Role>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<bool> RoleExists(string name)
        {
            return await _roleManager.RoleExistsAsync(name);
        }

        public async Task<IdentityResult> AssignRoleToUser(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> RemoveRoleToUser(User user, string role)
        {
            return await _userManager.RemoveFromRoleAsync(user, role);
        }
    }
}