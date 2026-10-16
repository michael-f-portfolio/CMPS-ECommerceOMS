using ECommerceOMS.Models;

namespace ECommerceOMS.ViewModels;

public class OrderEditViewModel
{
    public int Id { get; set; }
    public string PaymentStatus { get; set; }
    public string OrderStatus { get; set; }
    
    public string BuyerEmail { get; set; } = string.Empty;

    public decimal TotalCost { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}