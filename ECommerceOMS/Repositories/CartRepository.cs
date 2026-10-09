using ECommerceOMS.Data;
using ECommerceOMS.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Repositories;

public class CartRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CartRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Cart>> GetAllAsync() =>
        await _dbContext.Carts
            .Include(c => c.Buyer)
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .ToListAsync();

    public async Task<Cart?> GetByBuyerIdAsync(string buyerId) =>
        await _dbContext.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .ThenInclude(p => p.Seller)
            .FirstOrDefaultAsync(c => c.BuyerId == buyerId);

    public async Task<Cart> CreateAsync(string buyerId)
    {
        var cart = new Cart
        {
            BuyerId = buyerId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        _dbContext.Carts.Add(cart);
        await _dbContext.SaveChangesAsync();
        return cart;
    }
    
    public async Task ClearCartAsync(int cartId)
    {
        var cart = await _dbContext.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart == null)
            return;
        
        cart.Items.Clear();
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task SaveChangesAsync() => 
        await _dbContext.SaveChangesAsync();
}