using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    public class OrderManagementController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            throw new NotImplementedException();
        }
    }
}
