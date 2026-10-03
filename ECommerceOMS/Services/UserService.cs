using ECommerceOMS.Models.Identity;
using ECommerceOMS.Repositories;

namespace ECommerceOMS.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<ApplicationUser?> GetByIdAsync(string id)
            => _userRepository.FindByIdAsync(id);

        public Task<ApplicationUser?> GetByEmailAsync(string email)
            => _userRepository.FindByEmailAsync(email);

        public async Task<string?> GetRoleAsync(string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            if (user == null)
                return null;

            return await _userRepository.GetRoleAsync(user);
        }

        public Task<List<ApplicationUser>> GetAllUsersAsync()
                => _userRepository.GetAllUsersAsync();
    }
}
