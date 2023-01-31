using Blazor_App.Shared.DTOs;

namespace Blazor_App.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductResponseDto>>> GetCartProducts(List<CartItem> cartItems);
    }
}
