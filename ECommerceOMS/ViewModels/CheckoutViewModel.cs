using System.ComponentModel.DataAnnotations;
using ECommerceOMS.Models;

namespace ECommerceOMS.ViewModels;

public class CheckoutViewModel
{
    [Required]
    public string CardHolderName  { get; set; } = null!;
    [Required, StringLength(19, MinimumLength = 12)]
    public string CardNumber { get; set; } = null!;
    [Required, RegularExpression(@"^\d{2}/\d{2}$")]
    public string ExpirationDate { get; set; } = null!;
    [Required, StringLength(4, MinimumLength = 3)]
    public string Cvv { get; set; } = null!;
    [Required]
    public string BillingStreet  { get; set; } = null!;
    [Required]
    public string BillingCity { get; set; } = null!;
    [Required]
    public string BillingProvince { get; set; } = null!;
    [Required]
    public string BillingCountry { get; set; } = null!;
    [Required]
    public string BillingPostalCode { get; set; } = null!;
}