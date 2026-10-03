using ECommerceOMS.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerceOMS.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;
        [Range(0.01, 100000)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int QuantityOnHand { get; set; }
        public byte[]? ImageData { get; set; }
        public bool IsActive { get; set; }
        public string? SellerId { get; set; }
        public ApplicationUser? Seller { get; set; }
    }
}
