using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
