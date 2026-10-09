using ECommerceOMS.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Data.Seed
{
    public static class UserSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var superAdmin = new ApplicationUser
            {
                Id = "10000000-0000-0000-0000-000000000001",
                UserName = "superadmin@test.com",
                NormalizedUserName = "SUPERADMIN@TEST.COM",
                Email = "superadmin@test.com",
                NormalizedEmail = "SUPERADMIN@TEST.COM",
                EmailConfirmed = true,
                DisplayName = "Test Super Admin",
                SecurityStamp = "50000000-0000-0000-0000-000000000001",
                ConcurrencyStamp = "70000000-0000-0000-0000-000000000001",
                PasswordHash = "AQAAAAIAAYagAAAAEOVoN5jpbRkKNaBQCPfpqaB6VBH8EmlnqrgDZ6jozuPouLV6EaHYGxUn46gf3GktYQ=="
            };

            var admin = new ApplicationUser
            {
                Id = "10000000-0000-0000-0000-000000000002",
                UserName = "admin@test.com",
                NormalizedUserName = "ADMIN@TEST.COM",
                Email = "admin@test.com",
                NormalizedEmail = "ADMIN@TEST.COM",
                EmailConfirmed = true,
                DisplayName = "Test Administrator",
                SecurityStamp = "50000000-0000-0000-0000-000000000002",
                ConcurrencyStamp = "70000000-0000-0000-0000-000000000002",
                PasswordHash = "AQAAAAIAAYagAAAAENhdpnO5oUWM+s882qifac7x5CvslMHa5SWHWfUGR/cz238vsoG7brjZ5saQLEORqg=="
            };

            var buyer = new ApplicationUser
            {
                Id = "10000000-0000-0000-0000-000000000003",
                UserName = "buyer@test.com",
                NormalizedUserName = "BUYER@TEST.COM",
                Email = "buyer@test.com",
                NormalizedEmail = "BUYER@TEST.COM",
                EmailConfirmed = true,
                DisplayName = "Test Buyer",
                SecurityStamp = "50000000-0000-0000-0000-000000000003",
                ConcurrencyStamp = "70000000-0000-0000-0000-000000000003",
                PasswordHash = "AQAAAAIAAYagAAAAEJ5A6iOUDcwdwHZQqBhl7ZnuJ4s8nMKvrM4JcwOJgrQ4Mmqkcw9s6tRzDymHNh+y8w=="
            };

            var seller1 = new ApplicationUser
            {
                Id = "11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                UserName = "seller1@test.com",
                NormalizedUserName = "SELLER1@TEST.COM",
                Email = "seller1@test.com",
                NormalizedEmail = "SELLER1@TEST.COM",
                EmailConfirmed = true,
                DisplayName = "Test Seller 1",
                SecurityStamp = "50000000-0000-0000-0000-000000000004",
                ConcurrencyStamp = "70000000-0000-0000-0000-000000000004",
                PasswordHash = "AQAAAAIAAYagAAAAEJOVpDItScCqrZ0giYqMBLdIR5/eA94iD/XAzJ9XcYpI0zujjl1xJzUC6B6QiA7zCw=="
            };

            var seller2 = new ApplicationUser
            {
                Id = "22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                UserName = "seller2@test.com",
                NormalizedUserName = "SELLER2@TEST.COM",
                Email = "seller2@test.com",
                NormalizedEmail = "SELLER2@TEST.COM",
                EmailConfirmed = true,
                DisplayName = "Test Seller 2",
                SecurityStamp = "50000000-0000-0000-0000-000000000005",
                ConcurrencyStamp = "70000000-0000-0000-0000-000000000005",
                PasswordHash = "AQAAAAIAAYagAAAAEJOVpDItScCqrZ0giYqMBLdIR5/eA94iD/XAzJ9XcYpI0zujjl1xJzUC6B6QiA7zCw=="
            };

            var seller3 = new ApplicationUser
            {
                Id = "33333333-cccc-cccc-cccc-cccccccccccc",
                UserName = "seller3@test.com",
                NormalizedUserName = "SELLER3@TEST.COM",
                Email = "seller3@test.com",
                NormalizedEmail = "SELLER3@TEST.COM",
                EmailConfirmed = true,
                DisplayName = "Test Seller 3",
                SecurityStamp = "50000000-0000-0000-0000-000000000006",
                ConcurrencyStamp = "70000000-0000-0000-0000-000000000006",
                PasswordHash = "AQAAAAIAAYagAAAAEJOVpDItScCqrZ0giYqMBLdIR5/eA94iD/XAzJ9XcYpI0zujjl1xJzUC6B6QiA7zCw=="
            };

            modelBuilder.Entity<ApplicationUser>().HasData(
                superAdmin, admin, buyer, seller1, seller2, seller3
            );
        }
    }
}
