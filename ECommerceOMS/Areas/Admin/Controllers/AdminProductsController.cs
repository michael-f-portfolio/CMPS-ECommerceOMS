using ECommerceOMS.Models;
using ECommerceOMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    [Route("admin/products")]
    public class AdminProductsController : AdminBaseController
    {
        private readonly ProductService _productService;

        public AdminProductsController(ProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
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
            await _productService.DeleteAsync(id,  User);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("toggle/{id}"), ActionName("Toggle")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return RedirectToAction(nameof(Index));
            
            product.IsActive = !product.IsActive;
            await _productService.UpdateAsync(product,null, User);

            return RedirectToAction(nameof(Index));
        }
    }
}
