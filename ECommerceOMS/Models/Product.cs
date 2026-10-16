using ECommerceOMS.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Category { get; set; }
        [Range(0.01, 100000)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int QuantityOnHand { get; set; }
        public byte[]? ImageData { get; set; }
        public bool IsActive { get; set; }
        public string? SellerId { get; set; }
        public ApplicationUser? Seller { get; set; }

        [NotMapped]
        public decimal DiscountPercentage
        {
            get
            {
                if (QuantityOnHand > 40)
                    return 20m;
                if (QuantityOnHand > 20)
                    return 10m;

                return 0m;
            }
        }
        
        [NotMapped]
        public decimal DiscountedPrice =>
            Price * (1 - DiscountPercentage / 100);
    }

    public static class ProductCategories
    {
        public const string Peripherals = "Peripherals";
        public const string Components = "Components";
        public const string Monitors = "Monitors";
        public const string Accessories = "Accessories";
        public const string Furniture = "Furniture";

        public static readonly List<string> All =
        [
            Peripherals,
            Components,
            Monitors,
            Accessories,
            Furniture
        ];
    }
}
