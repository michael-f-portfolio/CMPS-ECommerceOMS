using System.Security.Claims;
using ECommerceOMS.Models;
using ECommerceOMS.Services;
using ECommerceOMS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Controllers;

public class CheckoutController : Controller
{
    private readonly OrderService _orderService;
    private readonly CartService _cartService;

    public CheckoutController(OrderService orderService, CartService cartService)
    {
        _orderService = orderService;
        _cartService = cartService;
    }
    
    private string GetBuyerId() =>
        User.FindFirstValue(ClaimTypes.NameIdentifier);

    [HttpGet]
    public IActionResult Index() =>
        View(new CheckoutViewModel());
    
    [HttpPost]
    public async Task<IActionResult> Index(CheckoutViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var buyerId = GetBuyerId();

        var order = new Order
        {
            
            BuyerId = buyerId,
            CreatedAt = DateTime.UtcNow,
            
            CardHolderName = viewModel.CardHolderName,
            CardNumberMasked = MaskCard(viewModel.CardNumber),
            Expiry = viewModel.ExpirationDate,
            
            BillingStreet = viewModel.BillingStreet,
            BillingCity = viewModel.BillingCity,
            BillingProvince = viewModel.BillingProvince,
            BillingCountry = viewModel.BillingCountry,
            BillingPostalCode = viewModel.BillingPostalCode,
            
            PaymentStatus = "Paid",
            OrderStatus = "Pending"
        };
        
        try
        {
            var createdOrder = await _orderService.CreateOrderFromCartAsync(order);
            return RedirectToAction("Details", "Orders", new { id = createdOrder.Id });
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
            return View(viewModel);
        }
    }

    public async Task<IActionResult> Checkout()
    {
        var buyerId = GetBuyerId();
        var cart = await _cartService.GetOrCreateCartAsync(buyerId);
        
        if (cart == null || cart.Items.Count == 0)
            return RedirectToAction(nameof(Index));

        var checkoutViewModel = new CheckoutViewModel
        {
            /*cart = cart*/
        };
        
        return View(checkoutViewModel);
    }
    
    private static string MaskCard(string cardNumber)
    {
        var last4 = cardNumber[^4..];
        return $"**** **** **** {last4}";
    }
}