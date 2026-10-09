using ECommerceOMS.Models;
using ECommerceOMS.Repositories;

namespace ECommerceOMS.Services;

public class CartService
{
    private readonly CartRepository  _cartRepository;
    private readonly ProductRepository _productRepository;

    public CartService(CartRepository cartRepository, ProductRepository productRepository)
    {
        _cartRepository  = cartRepository;
        _productRepository = productRepository;
    }

    public async Task<Cart> GetOrCreateCartAsync(string buyerId)
    {
        var cart = await _cartRepository.GetByBuyerIdAsync(buyerId);
        if (cart != null)
            return cart;
        
        return await _cartRepository.CreateAsync(buyerId);
    }

    public async Task<Cart> AddItemAsync(string buyerId, int productId, int quantity)
    {
        var cart = await GetOrCreateCartAsync(buyerId);
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
            throw new Exception("Product not found.");

        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = productId,
                Quantity = quantity,
                PriceAtAdd = product.Price
            });
        }

        cart.UpdatedAt = DateTime.UtcNow;
        await _cartRepository.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart> UpdateItemAsync(string buyerId, int productId, int quantity)
    {
        var cart = await GetOrCreateCartAsync(buyerId);
        var item = cart.Items.FirstOrDefault(i => i.Id == productId);

        if (item == null)
            throw new Exception("Cart item not found.");
        
        if (quantity <= 0)
            cart.Items.Remove(item);
        else
            item.Quantity = quantity;
        
        cart.UpdatedAt = DateTime.UtcNow;
        await _cartRepository.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart> RemoveItemAsync(string buyerId, int productId)
    {
        var cart = await GetOrCreateCartAsync(buyerId);
        var item = cart.Items.FirstOrDefault(i => i.Id == productId);
        
        if (item != null)
            cart.Items.Remove(item);
        
        cart.UpdatedAt = DateTime.UtcNow;
        await _cartRepository.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart?> GetCartAsync(string buyerId)
    {
        return await _cartRepository.GetByBuyerIdAsync(buyerId);
    }
    
}