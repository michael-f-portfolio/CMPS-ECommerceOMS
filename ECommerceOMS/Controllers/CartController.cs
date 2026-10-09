using System.Security.Claims;
using ECommerceOMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Controllers;

[Authorize(Roles = "Buyer")]
[Route("cart")]
public class CartController : Controller
{
    private readonly CartService _cartService;
    
    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }
    
    private string GetBuyerId() =>
        User.FindFirstValue(ClaimTypes.NameIdentifier);

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var cart = await _cartService.GetOrCreateCartAsync(GetBuyerId());

        if (_cartService.CartWasAdjusted)
        {
            TempData["CartWasAdjusted"] = "Some items were updated due to inventory changes";
        }
        
        return View(cart);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(int productId, int quantity = 1)
    {
        await _cartService.AddItemAsync(GetBuyerId(), productId, quantity);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(int itemId, int quantity)
    {
        await _cartService.UpdateItemAsync(GetBuyerId(),  itemId, quantity);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("remove")]
    public async Task<IActionResult> Remove(int itemId, int quantity)
    {
        await _cartService.RemoveItemAsync(GetBuyerId(), itemId);
        return RedirectToAction(nameof(Index));
    }
}