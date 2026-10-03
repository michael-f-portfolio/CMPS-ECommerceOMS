using Microsoft.AspNetCore.Identity;

namespace ECommerceOMS.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName {  get; set; }
    }
}
