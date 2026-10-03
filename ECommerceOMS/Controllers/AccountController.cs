using ECommerceOMS.Models.Identity;
using ECommerceOMS.Services;
using ECommerceOMS.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly UserService _userService;

        public AccountController(AccountService accountService, UserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()  
            => User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") 
                                             : View();
        

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            if (!await _accountService.LoginAsync(model))
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View(model);
            }

            var role = await _userService.GetRoleAsync(model.Email);

            return role switch
            {
                nameof(RoleType.SuperAdmin) => RedirectToAction("Index", "SuperAdmin"),
                nameof(RoleType.Admin) => RedirectToAction("Index", "Admin"),
                nameof(RoleType.Seller) => RedirectToAction("Index", "Seller"),
                _ => RedirectToAction("Index", "Buyer")
            };
        }

        [HttpGet]
        public IActionResult Register() => 
            User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") 
                                          : View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!await _accountService.RegisterAsync(model))
            {
                ModelState.AddModelError("", "Registration Failed");
                return View(model);
            }

            return RedirectToAction("Index", "Buyer");
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}