using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin, Buyer")]
    public class BuyerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
