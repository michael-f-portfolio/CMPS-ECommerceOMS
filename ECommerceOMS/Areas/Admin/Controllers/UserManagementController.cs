using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    public class UserManagementController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
