using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Data.Seed
{
    public static class RoleSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "00000000-0000-0000-0000-000000000001",
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    ConcurrencyStamp = "60000000-0000-0000-0000-000000000001"
                },
                new IdentityRole
                {
                    Id = "00000000-0000-0000-0000-000000000002",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "60000000-0000-0000-0000-000000000002"
                },
                new IdentityRole
                {
                    Id = "00000000-0000-0000-0000-000000000003",
                    Name = "Seller",
                    NormalizedName = "SELLER",
                    ConcurrencyStamp = "60000000-0000-0000-0000-000000000003"
                },
                new IdentityRole
                {
                    Id = "00000000-0000-0000-0000-000000000004",
                    Name = "Buyer",
                    NormalizedName = "BUYER",
                    ConcurrencyStamp = "60000000-0000-0000-0000-000000000004"
                }
            );
        }
    }
}