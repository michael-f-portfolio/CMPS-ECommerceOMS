using System.ComponentModel.DataAnnotations;

namespace ECommerceOMS.ViewModels;

public class ProductCreateViewModel
{
    [Required(ErrorMessage = "Product name is required.")] 
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Product description is required.")] 
    public string Description { get; set; } = null!;
    [Required(ErrorMessage = "Product category is required.")] 
    public string Category { get; set; } = null!;
    [Required(ErrorMessage = "Product price is required.")]
    [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100000.")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Product quantity on hand is required.")]
    [Range(0, int.MaxValue,  ErrorMessage = "Product quantity on hand must be between 0 and 2147483647.")]
    public int QuantityOnHand { get; set; }
    [Required(ErrorMessage = "Product image is required.")] 
    public IFormFile ImageFile { get; set; } = null!;
}