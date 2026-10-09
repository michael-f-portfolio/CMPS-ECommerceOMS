using ECommerceOMS.Data.Seed;
using ECommerceOMS.Models;
using ECommerceOMS.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ECommerceOMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem>  CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            RoleSeed.Seed(modelBuilder);
            UserSeed.Seed(modelBuilder);
            UserRoleSeed.Seed(modelBuilder);
            ProductSeed.Seed(modelBuilder);
        }
    }
}
