using ECommerceOMS.Models.Identity;
using ECommerceOMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    [Route("admin/users")]
    public class UserManagementController : AdminBaseController
    {
        private readonly UserService _userService;
        private readonly AdminService _adminService;

        public UserManagementController(UserService userService, AdminService adminService)
        {
            _userService = userService;
            _adminService = adminService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();

            users = users.Where(u =>
            {
                var roles = _userService.GetRoleAsync(u.Email).Result;
                return roles.Contains(RoleType.Buyer.ToName()) ||
                        roles.Contains(RoleType.Seller.ToName());
            }).ToList();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string email, RoleType newRole)
        {
            var actingUserEmail = User.Identity?.Name;
            if (actingUserEmail == null)
                return Unauthorized();

            var success = await _adminService.ChangeRoleAsync(actingUserEmail, email, newRole);
            if (!success)
                TempData["Error"] = "Role change not allowed.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleLock(string email)
        {
            var actingEmail = User.Identity?.Name;
            var success = await _adminService.ToggleLockAsync(actingEmail, email);

            if (!success)
                TempData["Error"] = "Unable to toggle lock";

            return RedirectToAction(nameof(Index));
        }
    }
}
