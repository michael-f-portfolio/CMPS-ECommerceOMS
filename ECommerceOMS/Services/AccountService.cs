using ECommerceOMS.Models.Identity;
using ECommerceOMS.Repositories;
using ECommerceOMS.ViewModels.Account;
using Microsoft.AspNetCore.Identity;

namespace ECommerceOMS.Services
{
    public class AccountService
    {
        private readonly UserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserRepository userRepository, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                false,
                false
                );

            return result.Succeeded;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model) 
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                DisplayName = model.DisplayName,
                EmailConfirmed = true
            };

            var created = await _userRepository.CreateAsync(user, model.Password);
            
            if (!created) 
                return false;

            await _userRepository.AddToRoleAsync(user, RoleType.Buyer.ToName());
            await _signInManager.SignInAsync(user, false);

            return true;
        }

        public async Task<string?> GetRoleAsync(string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            if (user == null) return null;

            var role = await _userRepository.GetRoleAsync(user);
            if (role == null) return null;

            return role;
        }

        public async Task<bool> AssignRoleAsync(string actingUserEmail, string targetEmail, RoleType newRole)
        {
            var actingUser = await _userRepository.FindByEmailAsync(actingUserEmail);
            if (actingUser == null) return false;

            var actingRole = await _userRepository.GetRoleAsync(actingUser);
            if (actingRole == null)
                return false;

            var targetUser = await _userRepository.FindByEmailAsync(targetEmail);
            if (targetUser == null) 
                return false;

            var targetUserCurrentRole = await _userRepository.GetRoleAsync(targetUser);

            // Has SuperAdmin Role - Can modify any role
            if (actingRole == RoleType.SuperAdmin.ToName())
            {
                if (targetUserCurrentRole != null)
                    await _userRepository.RemoveFromRoleAsync(targetUser, targetUserCurrentRole);

                return await _userRepository.AddToRoleAsync(targetUser, newRole.ToName());
            }

            // Has Admin Role - Can modify Buyer and Seller roles only
            if (actingRole == RoleType.Admin.ToName())
            {
                if (targetUserCurrentRole == RoleType.SuperAdmin.ToName() ||
                    targetUserCurrentRole == RoleType.Admin.ToName())
                    return false;

                if (targetUserCurrentRole == RoleType.Seller.ToName() ||
                    targetUserCurrentRole == RoleType.Buyer.ToName())
                    await _userRepository.RemoveFromRoleAsync(targetUser, targetUserCurrentRole);

                return await _userRepository.AddToRoleAsync(targetUser, newRole.ToName());
            }

            // Has Seller/Buyer Role - Cannot modify any roles
            return false;
        }

        public Task LogoutAsync() => _signInManager.SignOutAsync();
    }
}
