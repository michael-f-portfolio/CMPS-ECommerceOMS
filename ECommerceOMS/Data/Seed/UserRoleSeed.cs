using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Data.Seed
{
    public static class UserRoleSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "10000000-0000-0000-0000-000000000001",
                    RoleId = "00000000-0000-0000-0000-000000000001" // SuperAdmin
                },
                new IdentityUserRole<string>
                {
                    UserId = "10000000-0000-0000-0000-000000000002",
                    RoleId = "00000000-0000-0000-0000-000000000002" // Admin
                },
                new IdentityUserRole<string>
                {
                    UserId = "11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                    RoleId = "00000000-0000-0000-0000-000000000003" // Seller
                },
                new IdentityUserRole<string>
                {
                    UserId = "22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                    RoleId = "00000000-0000-0000-0000-000000000003" // Seller
                },
                new IdentityUserRole<string>
                {
                    UserId = "33333333-cccc-cccc-cccc-cccccccccccc",
                    RoleId = "00000000-0000-0000-0000-000000000003" // Seller
                },
                new IdentityUserRole<string>
                {
                    UserId = "10000000-0000-0000-0000-000000000003",
                    RoleId = "00000000-0000-0000-0000-000000000004" // Buyer
                }
            );
        }
    }
}