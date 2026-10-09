using ECommerceOMS.Data;
using ECommerceOMS.Models;
using ECommerceOMS.Models.Identity;
using ECommerceOMS.Repositories;

namespace ECommerceOMS.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly UserService _userService;
        
        public ProductService(ProductRepository productRepository, 
                              UserService userService)
        {
            _productRepository = productRepository;
            _userService = userService;
        }

        private async Task<bool> IsAdminAsync(ApplicationUser user)
        {
            var role = await _userService.GetRoleAsync(user.Email);
            return role == RoleType.Admin.ToName() || role == RoleType.SuperAdmin.ToName();
        }

        private async Task<bool> IsSellerAsync(ApplicationUser user)
        {
            var role = await _userService.GetRoleAsync(user.Id);
            return role == RoleType.Seller.ToName();
        }

        private static bool OwnsProduct(Product product, ApplicationUser user) 
            => product.Seller?.Id == user.Id;

        public Task<List<Product>> GetAllAsync() 
            => _productRepository.GetAllAsync();

        public Task<Product?> GetByIdAsync(int id)
            => _productRepository.GetByIdAsync(id);

        public async Task AddAsync(Product product, ApplicationUser currentUser)
        {
            if (!await IsSellerAsync(currentUser))
                throw new UnauthorizedAccessException("You don't have permission to add products.");
            
            product.SellerId = currentUser.Id;
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(Product updatedProduct, ApplicationUser currentUser)
        {
            var existing = await _productRepository.GetByIdAsync(updatedProduct.Id);
            if (existing == null)
                throw new FileNotFoundException("Product not found.");
            
            if (await IsAdminAsync(currentUser))
            {
                // Admins can edit any products
            }
            else if (await IsSellerAsync(currentUser))
            {   
                // Sellers can only edit their products
                if (!OwnsProduct(updatedProduct, currentUser))
                    throw new UnauthorizedAccessException("You cannot edit another seller's product.");
            }
            else
            {
                throw new UnauthorizedAccessException("You don't have permission to update this product.");
            }
            
            existing.Name = updatedProduct.Name;
            existing.Description =  updatedProduct.Description;
            existing.Price = updatedProduct.Price;
            existing.QuantityOnHand =  updatedProduct.QuantityOnHand;

            // New image data so update existing image
            if (updatedProduct.ImageData is { Length: > 0 })
            {
                existing.ImageData = updatedProduct.ImageData;
            }
            
            await _productRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(Product product, ApplicationUser currentUser)
        {
            if (await IsAdminAsync(currentUser))
            {
                await _productRepository.DeleteAsync(product);
                return;
            }
            
            if (!await IsSellerAsync(currentUser))
                throw new UnauthorizedAccessException("You don't have permission to delete this product.");
            
            if (!OwnsProduct(product, currentUser))
                throw new UnauthorizedAccessException("You cannot delete another seller's product.");
            
            await _productRepository.DeleteAsync(product);
        }
    }
}
