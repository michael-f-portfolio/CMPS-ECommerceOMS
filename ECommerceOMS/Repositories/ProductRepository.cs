using ECommerceOMS.Data;
using ECommerceOMS.Models;

namespace ECommerceOMS.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;

        ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public Task<List<Product>> GetAllAsync()
        //    => _context.Products.ToListAsync();
    }
}
