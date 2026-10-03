using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    public class AdminManagementController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
