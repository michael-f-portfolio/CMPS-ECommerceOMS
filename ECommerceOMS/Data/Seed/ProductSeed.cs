using ECommerceOMS.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Data;

public static class ProductSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            // Seller 1's products
            new Product {
                Id = 1,
                Name = "Mechanical Keyboard",
                Description = "RGB backlit mechanical keyboard with tactile switches.",
                Price = 129.99m,
                QuantityOnHand = 25,
                SellerId = "11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                IsActive = true
            },
            new Product {
                Id = 2,
                Name = "Wireless Gaming Mouse",
                Description = "High‑precision wireless gaming mouse with adjustable DPI.",
                Price = 89.99m,
                QuantityOnHand = 40,
                SellerId = "11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                IsActive = true
            },
            new Product {
                Id = 3,
                Name = "Webcam 1080p",
                Description = "Full HD webcam with autofocus and dual microphones.",
                Price = 59.99m,
                QuantityOnHand = 50,
                SellerId = "11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                IsActive = true
            },

            // Seller 2's products
            new Product {
                Id = 4,
                Name = "27\" 144Hz Monitor",
                Description = "IPS gaming monitor with 144Hz refresh rate and 1ms response time.",
                Price = 299.99m,
                QuantityOnHand = 15,
                SellerId = "22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                IsActive = true
            },
            new Product {
                Id = 5,
                Name = "NVIDIA RTX 4070 GPU",
                Description = "High‑performance graphics card for gaming and content creation.",
                Price = 699.99m,
                QuantityOnHand = 10,
                SellerId = "22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                IsActive = true
            },
            new Product {
                Id = 6,
                Name = "USB‑C Docking Station",
                Description = "Multi‑port docking station with HDMI, USB‑A, USB‑C, and Ethernet.",
                Price = 119.99m,
                QuantityOnHand = 30,
                SellerId = "22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                IsActive = true
            },
            new Product {
                Id = 7,
                Name = "Noise‑Cancelling Headset",
                Description = "Comfortable over‑ear headset with active noise cancellation.",
                Price = 159.99m,
                QuantityOnHand = 18,
                SellerId = "22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                IsActive = true
            },

            // Seller 3's products
            new Product {
                Id = 8,
                Name = "AMD Ryzen 7 7800X3D CPU",
                Description = "8‑core processor optimized for gaming performance.",
                Price = 449.99m,
                QuantityOnHand = 20,
                SellerId = "33333333-cccc-cccc-cccc-cccccccccccc",
                IsActive = true
            },
            new Product {
                Id = 9,
                Name = "External SSD 1TB",
                Description = "High‑speed portable SSD with USB‑C connectivity.",
                Price = 149.99m,
                QuantityOnHand = 35,
                SellerId = "33333333-cccc-cccc-cccc-cccccccccccc",
                IsActive = true
            },
            new Product {
                Id = 10,
                Name = "PC Case RGB Mid‑Tower",
                Description = "Tempered glass mid‑tower case with RGB fans included.",
                Price = 99.99m,
                QuantityOnHand = 22,
                SellerId = "33333333-cccc-cccc-cccc-cccccccccccc",
                IsActive = true
            },
            new Product {
                Id = 11,
                Name = "650W Modular Power Supply",
                Description = "80+ Gold certified fully modular PSU.",
                Price = 119.99m,
                QuantityOnHand = 28,
                SellerId = "33333333-cccc-cccc-cccc-cccccccccccc",
                IsActive = true
            },
            new Product {
                Id = 12,
                Name = "Gaming Chair",
                Description = "Ergonomic gaming chair with adjustable lumbar support.",
                Price = 199.99m,
                QuantityOnHand = 12,
                SellerId = "33333333-cccc-cccc-cccc-cccccccccccc",
                IsActive = true
            }
        );
    }
}
