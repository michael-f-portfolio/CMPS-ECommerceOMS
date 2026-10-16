using ECommerceOMS.Models;
using ECommerceOMS.Repositories;

namespace ECommerceOMS.Services;

public class OrderService
{
    private readonly CartRepository _cartRepository;
    private readonly ProductRepository _productRepository;
    private readonly OrderRepository _orderRepository;

    public OrderService(CartRepository cartRepository, 
                        ProductRepository productRepository,
                        OrderRepository orderRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _orderRepository.GetAllAsync();
    }

    public async Task<List<Order>> GetByBuyerIdAsync(string buyerId)
    {
        return await _orderRepository.GetByBuyerIdAsync(buyerId);
    }

    public async Task<Order> GetByIdAsync(int id/*, string buyerId*/)
    {
        return await _orderRepository.GetByIdAsync(id/*, buyerId*/);
    }
    
    public async Task<Order> CreateOrderFromCartAsync(Order order)
    {
        var cart = await _cartRepository.GetByBuyerIdAsync(order.BuyerId)
                   ?? throw new Exception("Cart not found.");
        
        // validate cart items against inventory
        foreach (var item in cart.Items.ToList())
        {
            var product = item.Product;
            if (product is not { IsActive: true })
                throw new Exception("Cart contains inactive or removed products.");
            
            if (item.Quantity > product.QuantityOnHand)
                throw new Exception("Cart contains items exceeding available inventory.");
        }
        
        foreach (var item in cart.Items.ToList())
        {
            var product = item.Product;
            product.QuantityOnHand -= item.Quantity;
            
            if (product.QuantityOnHand < 0)
                product.IsActive = false;
            
            order.Items.Add(new OrderItem
            {
                ProductId = product.Id,
                SellerId = product.SellerId,
                Quantity = item.Quantity,
                PriceAtPurchase = item.PriceAtAdd
            });
        }

        order.TotalCost = order.Items.Sum(i => i.PriceAtPurchase * i.Quantity);
        
        await _orderRepository.AddAsync(order);
        
        cart.Items.Clear();
        await _cartRepository.SaveChangesAsync();

        return order;
    }

    public async Task UpdateAsync(Order order)
    {
        await _orderRepository.UpdateAsync(order);
    }
}