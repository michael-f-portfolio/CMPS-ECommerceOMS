using ECommerceOMS.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerceOMS.Repositories
{
    public class UserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<ApplicationUser?> FindByEmailAsync(string email) 
            => _userManager.FindByEmailAsync(email);

        public Task<ApplicationUser?> FindByIdAsync(string id) 
            => _userManager.FindByIdAsync(id);

        public async Task<bool> AddToRoleAsync(ApplicationUser user, string role)
            => (await _userManager.AddToRoleAsync(user, role)).Succeeded;

        public async Task<bool> RemoveFromRoleAsync(ApplicationUser user, string role)
            => (await _userManager.RemoveFromRoleAsync(user, role)).Succeeded;

        public async Task<bool> CreateAsync(ApplicationUser user, string password)
            => (await _userManager.CreateAsync(user, password)).Succeeded;

        public async Task<string?> GetRoleAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }

        public Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return Task.FromResult(_userManager.Users.ToList());
        }
    }
}
