using ECommerceOMS.Models;
using ECommerceOMS.Services;
using ECommerceOMS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOMS.Areas.Admin.Controllers
{
    [Route("admin/orders")]
    public class OrderManagementController : AdminBaseController
    {
        private readonly OrderService  _orderService;

        public OrderManagementController(OrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllAsync();
            
            return View(orders);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            
            if (order == null)
                return RedirectToAction(nameof(Index));
            
            return View(new OrderEditViewModel
            {
                Id = order.Id,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                BuyerEmail = order.Buyer.Email,
                TotalCost = order.TotalCost,
                Items = order.Items
            });
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(OrderEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            
            var existingOrder = await _orderService.GetByIdAsync(viewModel.Id);
            
            if (existingOrder == null)
                return RedirectToAction(nameof(Index));
            
            existingOrder.OrderStatus = viewModel.OrderStatus;
            existingOrder.PaymentStatus = viewModel.PaymentStatus;

            await _orderService.UpdateAsync(existingOrder);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
