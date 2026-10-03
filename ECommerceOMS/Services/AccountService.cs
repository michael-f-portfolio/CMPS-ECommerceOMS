using ECommerceOMS.Models.Identity;
using ECommerceOMS.Repositories;
using ECommerceOMS.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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
            var user = await _userRepository.FindByEmailAsync(model.Email);
            if (user == null)
                return false;

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                model.Password,
                lockoutOnFailure: false);

            if (!result.Succeeded)
                return false;

            var claims = new List<Claim>
            {
                new Claim("DisplayName", user.DisplayName ?? user.UserName)
            };

            await _signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);

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

        public Task LogoutAsync() => _signInManager.SignOutAsync();
    }
}
