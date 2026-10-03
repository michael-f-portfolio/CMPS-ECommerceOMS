using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Controllers
{
    public class AccessDeniedController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Shared/AccessDenied.cshtml");
        }
    }
}
