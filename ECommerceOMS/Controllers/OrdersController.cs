using System.Security.Claims;
using ECommerceOMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Controllers;

public class OrdersController : Controller
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var orders = await _orderService
            .GetByBuyerIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return View(orders);
    }

    [HttpGet("details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderService.GetByIdAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
        return View(order);
    }
}