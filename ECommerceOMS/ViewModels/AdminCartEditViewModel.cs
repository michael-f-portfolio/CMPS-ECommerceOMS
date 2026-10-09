namespace ECommerceOMS.ViewModels;

public class AdminCartEditViewModel
{
    public int CartId { get; set; }
    public string BuyerId { get; set; }
    public string BuyerDisplayName { get; set; }
    public int ItemCount { get; set; }
    public decimal TotalValue { get; set; }
}