using System.Security.Claims;
using ECommerceOMS.Models;
using ECommerceOMS.Services;
using ECommerceOMS.ViewModels;
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
    public async Task<IActionResult> Create(ProductCreateViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);
        
        using var ms = new MemoryStream();
        await viewModel.ImageFile.CopyToAsync(ms);

        var product = new Product
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            Price = viewModel.Price,
            QuantityOnHand = viewModel.QuantityOnHand,
            ImageData =  ms.ToArray(),
            SellerId = GetSellerId()
        };
        
        await _productService.AddAsync(product, User);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return RedirectToAction(nameof(Index));

        var editViewModel = new ProductEditViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            QuantityOnHand = product.QuantityOnHand,
            IsActive =  product.IsActive,
            ExistingImageBase64 = product.ImageData != null
                ? Convert.ToBase64String(product.ImageData)
                : null
        };
        
        return View(editViewModel);
    }

    [HttpPost("edit/{id}")]
    public async Task<IActionResult> Edit(ProductEditViewModel  editViewModel)
    {
        if (!ModelState.IsValid)
            return View(editViewModel);
        
        var  product = await _productService.GetByIdAsync(editViewModel.Id);
        if  (product == null)
            return RedirectToAction(nameof(Index));
        
        product.Name = editViewModel.Name;
        product.Description = editViewModel.Description;
        product.Price = editViewModel.Price;
        product.QuantityOnHand = editViewModel.QuantityOnHand;
        product.IsActive = editViewModel.IsActive;

        if (editViewModel.ImageFile != null)
        {
            using var ms =  new MemoryStream();
            await editViewModel.ImageFile.CopyToAsync(ms);
            product.ImageData = ms.ToArray();
        }
        
        await _productService.UpdateAsync(product, User);
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
        await _productService.UpdateAsync(product, User);
        
        return RedirectToAction(nameof(Index));
    }
    
    
}