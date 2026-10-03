using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    public class ProductsController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
