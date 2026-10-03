using ECommerceOMS.Models.Identity;
using ECommerceOMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    public class UserManagementController : AdminBaseController
    {
        private readonly UserService _userService;
        private readonly AdminService _adminService;

        public UserManagementController(UserService userService, AdminService adminService)
        {
            _userService = userService;
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();

            if (User.IsInRole(RoleType.Admin.ToName()))
            {
                users = users.Where(u =>
                {
                    var roles = _userService.GetRoleAsync(u.Email).Result;
                    return roles.Contains(RoleType.Buyer.ToName()) ||
                           roles.Contains(RoleType.Seller.ToName());
                }).ToList();
            }

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string targetEmail, RoleType newRole)
        {
            var actingUserEmail = User.Identity?.Name;
            if (actingUserEmail == null)
                return Unauthorized();

            var success = await _adminService.AssignRoleAsync(actingUserEmail, targetEmail, newRole);
            if (!success)
                TempData["Error"] = "Role change not allowed.";

            return RedirectToAction(nameof(Index));
        }
    }
}
