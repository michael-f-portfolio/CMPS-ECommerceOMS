using System.ComponentModel.DataAnnotations;

namespace ECommerceOMS.ViewModels;

public class ProductCreateViewModel
{
    [Required] 
    public string Name { get; set; } = null!;
    [Required] 
    public string Description { get; set; } = null!;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int QuantityOnHand { get; set; }
    [Required] 
    public IFormFile ImageFile { get; set; } = null!;
}