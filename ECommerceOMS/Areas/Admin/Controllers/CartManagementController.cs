using ECommerceOMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    [Route("admin/carts")]
    public class CartManagementController : AdminBaseController
    {
        private readonly CartService _cartService;

        public CartManagementController(CartService cartService)
        {
            _cartService = cartService;
        }
        
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var carts = await _cartService.GetAllCartsForAdminAsync();
            return View(carts);
        }

        [HttpPost]
        public async Task<IActionResult> Clear(int cartId)
        {
            await _cartService.ClearCartAsync(cartId);
            TempData["Message"] = "Cart cleared";
            return RedirectToAction(nameof(Index));
        }
    }
}
