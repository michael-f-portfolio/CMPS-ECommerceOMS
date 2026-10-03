using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    public class CartsController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
