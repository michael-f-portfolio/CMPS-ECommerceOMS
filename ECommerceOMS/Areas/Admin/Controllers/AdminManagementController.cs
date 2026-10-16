using ECommerceOMS.Models.Identity;
using ECommerceOMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [Route("admin/admins")]
    public class AdminManagementController : AdminBaseController
    {
        private readonly UserService _userService;
        private readonly AdminService _adminService;

        public AdminManagementController(UserService userService, AdminService adminService)
        {
            _userService = userService;
            _adminService = adminService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var admins = await _adminService.GetAdminsAsync();
            return View(admins);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string email, RoleType newRole)
        {
            var actingUserEmail = User.Identity?.Name;
            if (actingUserEmail == null)
                return Unauthorized();
            
            var success =  await _adminService.ChangeRoleAsync(actingUserEmail, email, newRole);
            if (!success)
                TempData["Error"] = "Role change not allowed.";

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost("toggle")]
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
