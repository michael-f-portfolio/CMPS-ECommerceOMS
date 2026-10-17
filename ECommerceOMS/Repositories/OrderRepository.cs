using ECommerceOMS.Data;
using ECommerceOMS.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Repositories;

public class OrderRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        var orders = await _dbContext.Orders
            .Include(o => o.Buyer)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .OrderBy(o => o.CreatedAt)
            .ToListAsync();
        return orders;
    }
    
    public async Task<List<Order>> GetByBuyerIdAsync(string buyerId) =>
        await _dbContext.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .Where(o => o.BuyerId == buyerId)
            .OrderBy(o => o.CreatedAt)
            .ToListAsync();

    public async Task<Order?> GetByIdAsync(int id/*, string buyerId*/) =>
        await _dbContext.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .ThenInclude(p => p.Seller)
            .Include(o => o.Buyer)
            .FirstOrDefaultAsync(o => o.Id ==  id /*&& o.BuyerId == buyerId*/);

    public async Task AddAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
    }

    public async Task UpdateAsync(Order order)
    {
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
    }
}