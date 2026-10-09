using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ECommerceOMS.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName {  get; set; }
        [NotMapped]
        public bool isLocked => 
            LockoutEnd != null && LockoutEnd > DateTimeOffset.UtcNow;

    }
}
