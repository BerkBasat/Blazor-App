using Blazor_App.Shared.DTOs;
using Blazor_App.Shared.Models;

namespace Blazor_App.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(CartItem cartItem);
        Task<List<CartItem>> GetCartItems();
        Task<List<CartProductResponseDto>> GetCartProducts();
        Task RemoveProductFromCart(int productId, int productTypeId);
        Task UpdateQuantity(CartProductResponseDto product);
        Task StoreCartItems(bool emptyLocalCart);
    }
}
