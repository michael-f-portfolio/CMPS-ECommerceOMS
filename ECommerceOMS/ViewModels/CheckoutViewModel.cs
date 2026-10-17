using System.ComponentModel.DataAnnotations;
using ECommerceOMS.Models;

namespace ECommerceOMS.ViewModels;

public class CheckoutViewModel
{
    [Required(ErrorMessage = "Cardholder name is required.")]
    public string CardHolderName  { get; set; } = null!;
    [Required(ErrorMessage = "Card number is required."), 
     StringLength(19, MinimumLength = 12, ErrorMessage = "16 digit card number is required.")]
    public string CardNumber { get; set; } = null!;
    [Required(ErrorMessage = "Expiration date is required."), 
     RegularExpression(@"^\d{2}/\d{2}$", ErrorMessage = "Expiration date must be a valid date (MM/YY).")]
    public string ExpirationDate { get; set; } = null!;
    [Required(ErrorMessage = "CVV is required."), 
     StringLength(4, MinimumLength = 3, ErrorMessage = "CVV must be 3 or 4 digits.")]
    public string Cvv { get; set; } = null!;
    [Required(ErrorMessage = "Address is required."),]
    public string BillingStreet  { get; set; } = null!;
    [Required(ErrorMessage = "City is required."),]
    public string BillingCity { get; set; } = null!;
    [Required(ErrorMessage = "Province is required."),]
    public string BillingProvince { get; set; } = null!;
    [Required(ErrorMessage = "Country is required."),]
    public string BillingCountry { get; set; } = null!;
    [Required(ErrorMessage = "PostalCode is required."), 
    RegularExpression(@"^[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ] ?\d[ABCEGHJKLMNPRSTVWXYZ]\d$",
     ErrorMessage = "Invalid postal code.")]
    public string BillingPostalCode { get; set; } = null!;
}