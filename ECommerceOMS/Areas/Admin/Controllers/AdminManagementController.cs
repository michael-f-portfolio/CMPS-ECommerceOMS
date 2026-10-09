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

        public async Task<IActionResult> Index()
        {
            var admins = await _adminService.GetAdminsAsync();
            return View(admins);
        }

        [HttpPost]
        public async Task<IActionResult> PromoteToSuperAdmin(string email)
        {
            var actingUserEmail = User.Identity?.Name;
            if (actingUserEmail == null)
                return Unauthorized();

            if (actingUserEmail == email)
                return Unauthorized();

            var success = await _adminService.ChangeRoleAsync(actingUserEmail, email, Models.Identity.RoleType.SuperAdmin);
            if (!success)
                TempData["Error"] = "Promotion not allowed.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DemoteToAdmin(string email)
        {
            var actingUserEmail = User.Identity?.Name;
            if (actingUserEmail == null)
                return Unauthorized();

            if (actingUserEmail == email)
                return Unauthorized();

            var success = await _adminService.ChangeRoleAsync(actingUserEmail, email, Models.Identity.RoleType.Admin);
            if (!success)
                TempData["Error"] = "Promotion not allowed.";

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
