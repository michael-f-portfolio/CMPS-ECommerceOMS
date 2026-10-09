using ECommerceOMS.Models.Identity;

namespace ECommerceOMS.Models;

public class Cart
{
    public int Id { get; set; }
    
    public string BuyerId { get; set; }
    public ApplicationUser  Buyer { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}