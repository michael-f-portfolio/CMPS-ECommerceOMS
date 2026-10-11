using ECommerceOMS.Models.Identity;

namespace ECommerceOMS.Models;

public class Order
{
    public int Id { get; set; }
    public string? BuyerId { get; set; }
    public ApplicationUser? Buyer { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public decimal TotalCost { get; set; }

    public string PaymentStatus { get; set; } = "Paid";
    public string OrderStatus { get; set; } = "Pending";
    
    public string CardHolderName { get; set; } = null!;
    public string CardNumberMasked { get; set; } = null!;
    public string Expiry { get; set; } = null!;
    public string BillingStreet { get; set; } = null!;
    public string BillingCity { get; set; } = null!;
    public string BillingProvince  { get; set; } = null!;
    public string BillingCountry  { get; set; } = null!;
    public string BillingPostalCode { get; set; } = null!;

    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}

public class OrderItem
{
    public int Id { get; set; }
    
    public int? OrderId { get; set; }
    public Order? Order { get; set; }
    
    public int? ProductId { get; set; }
    public Product? Product { get; set; }
    
    public string? SellerId  { get; set; }
    public ApplicationUser? Seller { get; set; }
    
    public int Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
}