using System.Security.Claims;
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
            var role = await _userService.GetRoleAsync(user.Email);
            return role == RoleType.Seller.ToName();
        }

        private static bool OwnsProduct(Product product, ApplicationUser user)
        {
            // No Seller assigned means a new product
            if (product.Seller == null)
                return true;

            return product.Seller?.Id == user.Id;
        }

        private async Task ValidatePermissions(Product product, ApplicationUser currentUser)
        {
            if (await IsAdminAsync(currentUser))
            {
                // Admins can edit any products
            }
            else if (await IsSellerAsync(currentUser))
            {   
                // Sellers can only edit their products
                if (!OwnsProduct(product, currentUser))
                    throw new UnauthorizedAccessException("You cannot edit another seller's product.");
            }
            else
            {
                throw new UnauthorizedAccessException("You don't have permission to update this product.");
            }
        }
        
        public Task<List<Product>> GetAllAsync() => 
            _productRepository.GetAllAsync();

        public Task<Product?> GetByIdAsync(int id) => 
            _productRepository.GetByIdAsync(id);

        public Task<List<Product>> GetProductsBySellerIdAsync(string sellerId) => 
            _productRepository.GetProductsBySellerIdAsync(sellerId);

        public Task<List<Product>> GetAllActiveAsync() => 
            _productRepository.GetAllActiveAsync();

        public async Task AddAsync(Product product, IFormFile? imageFile, ClaimsPrincipal? claimsPrincipal)
        {
            if (claimsPrincipal == null)
                throw new UnauthorizedAccessException("You don't have permission to add products.");
 
            var currentUser = await _userService.GetCurrentUserAsync(claimsPrincipal);
 
            // Assign this new product to the current user
            product.SellerId = currentUser.Id;
            
            try
            {
                await ValidatePermissions(product, currentUser);
            }
            catch (UnauthorizedAccessException exception)
            {
                throw new UnauthorizedAccessException(exception.Message);
            }
            
            if (imageFile is { Length: > 0 })
            {
                using var ms = new MemoryStream();
                await imageFile.CopyToAsync(ms);
                product.ImageData = ms.ToArray();
            }
            
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(Product updatedProduct, IFormFile? imageFile, ClaimsPrincipal? claimsPrincipal)
        {
            var existingProduct = await _productRepository.GetByIdAsync(updatedProduct.Id);

            if (existingProduct == null)
                throw new FileNotFoundException("Product not found.");

            if (claimsPrincipal == null)
                throw new UnauthorizedAccessException("You don't have permission to edit this product.");
            
            var currentUser = await _userService.GetCurrentUserAsync(claimsPrincipal);
            
            try
            {
                await ValidatePermissions(existingProduct, currentUser);
            }
            catch (UnauthorizedAccessException exception)
            {
                throw new UnauthorizedAccessException(exception.Message);
            }
            
            if (imageFile is { Length: > 0 })
            {
                using var ms = new MemoryStream();
                await imageFile.CopyToAsync(ms);
                updatedProduct.ImageData = ms.ToArray();
            }
            
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description =  updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.QuantityOnHand =  updatedProduct.QuantityOnHand;
            existingProduct.IsActive = updatedProduct.IsActive;
            
            // New image data so update existing image
            if (updatedProduct.ImageData is { Length: > 0 })
            {
                existingProduct.ImageData = updatedProduct.ImageData;
            }
            
            await _productRepository.UpdateAsync(existingProduct);
        }

        public async Task DeleteAsync(int productId, ClaimsPrincipal claimsPrincipal)
        {
            var productToDelete = await _productRepository.GetByIdAsync(productId);

            if (productToDelete  == null)
                throw new FileNotFoundException("Product not found.");
            
            if (claimsPrincipal  == null)
                throw new UnauthorizedAccessException("You don't have permission to delete this product.");

            var currentUser =  await _userService.GetCurrentUserAsync(claimsPrincipal);
            
            try
            {
                await ValidatePermissions(productToDelete, currentUser);
            }
            catch (UnauthorizedAccessException exception)
            {
                throw new UnauthorizedAccessException(exception.Message);
            }
            
            await _productRepository.DeleteAsync(productToDelete);
        }
    }
}
