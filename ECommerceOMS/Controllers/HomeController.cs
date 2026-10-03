using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
