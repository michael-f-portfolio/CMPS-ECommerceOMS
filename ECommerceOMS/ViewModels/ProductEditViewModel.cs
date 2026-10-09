using System.ComponentModel.DataAnnotations;

namespace ECommerceOMS.ViewModels;

public class ProductEditViewModel
{
    public int Id { get; set; }
    
    [Required] 
    public string Name { get; set; } = null!;
    [Required] 
    public string Description { get; set; } = null!;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int QuantityOnHand { get; set; }
    
    public IFormFile? ImageFile { get; set; } = null!;
    public bool IsActive { get; set; }
    public string? ExistingImageBase64  { get; set; }
}