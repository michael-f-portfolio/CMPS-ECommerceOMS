using System.Security.Claims;
using ECommerceOMS.Models;
using ECommerceOMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Controllers;

[Authorize(Roles = "Seller")]
[Route("seller/products")]
public class SellerController : Controller
{
    private readonly ProductService  _productService;

    public SellerController(ProductService productService)
    {
        _productService = productService;
    }

    private string GetSellerId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var sellerId = GetSellerId();
        var products = await _productService.GetProductsBySellerIdAsync(sellerId);
        return View(products);
    }

    [HttpGet("create")]
    public IActionResult Create() => View();

    [HttpPost("create")]
    public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
            return View(product);
        
        await _productService.AddAsync(product, imageFile, User);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return RedirectToAction(nameof(Index));
        
        return View(product);
    }

    [HttpPost("edit/{id}")]
    public async Task<IActionResult> Edit(Product product, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
            return View(product);
        
        await _productService.UpdateAsync(product, imageFile, User);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return RedirectToAction(nameof(Index));

        return View(product);
    }

    [HttpPost("delete/{id}"), ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.DeleteAsync(id, User);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("toggle/{id}"), ActionName("Toggle")]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return RedirectToAction(nameof(Index));

        product.IsActive = !product.IsActive;
        await _productService.UpdateAsync(product, null, User);
        
        return RedirectToAction(nameof(Index));
    }
    
    
}