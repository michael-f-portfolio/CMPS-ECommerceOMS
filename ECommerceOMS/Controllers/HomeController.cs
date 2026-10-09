using ECommerceOMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService  _productService;

        public HomeController(ProductService productService)
        {
            _productService = productService;
        }
        
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllActiveAsync();
            return View(products);
        }
    }
}
