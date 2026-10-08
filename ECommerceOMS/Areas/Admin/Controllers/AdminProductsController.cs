using ECommerceOMS.Models;
using ECommerceOMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    [Route("admin/products")]
    public class AdminProductsController : AdminBaseController
    {
        private readonly ProductService _productService;
        private readonly UserService _userService;

        public AdminProductsController(ProductService productService,  
                                       UserService userService)
        {
            _productService = productService;
            _userService = userService;
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

            if (imageFile is { Length: > 0 })
            {
                using var ms = new MemoryStream();
                await imageFile.CopyToAsync(ms);
                product.ImageData = ms.ToArray();
            }

            var currentUser = await _userService.GetCurrentUserAsync(User);
            await _productService.UpdateAsync(product, currentUser);
            
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
            var currentUser = await _userService.GetCurrentUserAsync(User);
            var product = await _productService.GetByIdAsync(id);
            if (product != null)
                await _productService.DeleteAsync(product,  currentUser);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("toggle/{id}"), ActionName("Toggle")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return RedirectToAction(nameof(Index));

            var currentUser = await _userService.GetCurrentUserAsync(User);
            product.IsActive = !product.IsActive;
            await _productService.UpdateAsync(product,  currentUser);

            return RedirectToAction(nameof(Index));
        }
    }
}
