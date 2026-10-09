using ECommerceOMS.Data;
using ECommerceOMS.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Product>> GetAllAsync() => 
            _dbContext.Products.Include(p => p.Seller).ToListAsync();
        
        public Task<Product?> GetByIdAsync(int id) => 
            _dbContext.Products.Include(p => p.Seller).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<List<Product>> GetProductsBySellerIdAsync(string sellerId) => 
            await _dbContext.Products.Where(p => p.SellerId == sellerId).ToListAsync();

        public async Task<List<Product>> GetAllActiveAsync() =>
            await _dbContext.Products
                .Where(p => p.IsActive)
                .Include(p => p.Seller)
                .ToListAsync();
        
        public async Task AddAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product) 
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
