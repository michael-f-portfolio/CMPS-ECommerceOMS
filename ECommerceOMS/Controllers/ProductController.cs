using ECommerceOMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Controllers;

public class ProductController : Controller
{
    private readonly ProductService  _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }
    
    public IActionResult Index() =>  View();

    public async Task<IActionResult> GetProducts()
    {
        return View(await _productService.GetAllAsync());
    }
}