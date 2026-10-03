using ECommerceOMS.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerceOMS.Data
{
    
    public static class IdentitySeed
    {
        /// <summary>
        /// A static method to 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            // Add roles as defined by RoleType Enum to the RoleManager
            foreach (RoleType role in Enum.GetValues<RoleType>())
            {
                var roleName = role.ToName();

                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Add SuperAdmin/Admin/Buyer/Seller Seeded Users
            // Add SuperAdmin User
            var superAdminEmail = "superadmin@test.com";
            var superAdmin = await userManager.FindByNameAsync(superAdminEmail);

            if (superAdmin == null) 
            {
                superAdmin = new ApplicationUser
                {
                    UserName = superAdminEmail,
                    Email = superAdminEmail,
                    DisplayName = "Test Super Admin",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(superAdmin, "SuperAdmin123!");
                await userManager.AddToRoleAsync(superAdmin, RoleType.SuperAdmin.ToName());
            }

            // Add Admin User
            var adminEmail = "admin@test.com";
            var adminUser = await userManager.FindByNameAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    DisplayName = "Test Administrator"
                };

                await userManager.CreateAsync(adminUser, "Admin123!");
                await userManager.AddToRoleAsync(adminUser, RoleType.Admin.ToName());
            }

            // Add Buyer User
            var buyerEmail = "buyer@test.com";
            var buyerUser = await userManager.FindByNameAsync(buyerEmail);

            if (buyerUser == null) 
            {
                buyerUser = new ApplicationUser
                {
                    UserName = buyerEmail,
                    Email = buyerEmail,
                    EmailConfirmed = true,
                    DisplayName = "Test Buyer"
                };

                await userManager.CreateAsync(buyerUser, "Buyer123!");
                await userManager.AddToRoleAsync(buyerUser, RoleType.Buyer.ToName());
            }

            // Add Seller User
            var sellerEmail = "seller@test.com";
            var sellerUser = await userManager.FindByNameAsync(sellerEmail);

            if (sellerUser == null)
            {
                sellerUser = new ApplicationUser
                {
                    UserName = sellerEmail,
                    Email = sellerEmail,
                    EmailConfirmed = true,
                    DisplayName = "Test Seller"
                };

                await userManager.CreateAsync(sellerUser, "Seller123!");
                await userManager.AddToRoleAsync(sellerUser, RoleType.Seller.ToName());
            }
        }
    }
}
