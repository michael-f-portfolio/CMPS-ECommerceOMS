using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    public class OrdersController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
