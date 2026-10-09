using ECommerceOMS.Models;
using ECommerceOMS.Repositories;
using ECommerceOMS.ViewModels;

namespace ECommerceOMS.Services;

public class CartService
{
    private readonly CartRepository  _cartRepository;
    private readonly ProductRepository _productRepository;
    
    public bool CartWasAdjusted { get; private set; }

    public CartService(CartRepository cartRepository, ProductRepository productRepository)
    {
        _cartRepository  = cartRepository;
        _productRepository = productRepository;
    }

    public async Task<Cart> GetOrCreateCartAsync(string buyerId)
    {
        var cart = await _cartRepository.GetByBuyerIdAsync(buyerId)
                    ?? await _cartRepository.CreateAsync(buyerId);

        CartWasAdjusted = false;

        // check if state of products in cart have changed 
        foreach (var item in cart.Items.ToList())
        {
            var product = item.Product;

            // product deleted or marked inactive
            // remove
            if (product is not { IsActive: true })
            {
                cart.Items.Remove(item);
                CartWasAdjusted = true;
                continue;
            }

            // product inventory reduced elsewhere
            // clamp down CartItem quantity to quantity on hand
            if (item.Quantity > product.QuantityOnHand)
            {
                item.Quantity = product.QuantityOnHand;
                CartWasAdjusted = true;
            }

            // total product inventory is 0
            // remove from cart
            if (product.QuantityOnHand <= 0)
            {
                cart.Items.Remove(item);
                CartWasAdjusted = true;
            }
        }

        if (CartWasAdjusted)
        {
            await _cartRepository.SaveChangesAsync();
        }

        return cart;
    }
    
    public async Task<List<AdminCartEditViewModel>> GetAllCartsForAdminAsync()
    {
        var carts = await _cartRepository.GetAllAsync();

        return carts.Select(c => new AdminCartEditViewModel
        {
            CartId = c.Id,
            BuyerId = c.BuyerId,
            BuyerDisplayName =  c.Buyer?.DisplayName,
            ItemCount = c.Items.Count,
            TotalValue = c.Items.Sum(i => i.PriceAtAdd * i.Quantity)
        }).ToList();
    }

    public async Task<Cart> AddItemAsync(string buyerId, int productId, int quantity)
    {
        var cart = await GetOrCreateCartAsync(buyerId);
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is not { IsActive: true })
            throw new Exception("Product not found.");

        var maxQty = product.QuantityOnHand;
        
        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity = Math.Min(existingItem.Quantity + quantity, maxQty);
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = productId,
                Quantity = Math.Min(quantity, maxQty),
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
        
        var product = item.Product;
        if (product is not { IsActive: true })
        {
            cart.Items.Remove(item);
            await _cartRepository.SaveChangesAsync();
            return cart;
        }
        
        var maxQty = product.QuantityOnHand;
        
        if (quantity <= 0)
            cart.Items.Remove(item);
        else
            item.Quantity = Math.Min(quantity, maxQty);
        
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

    public async Task ClearCartAsync(int cartId)
    {
        await _cartRepository.ClearCartAsync(cartId);
    }


}