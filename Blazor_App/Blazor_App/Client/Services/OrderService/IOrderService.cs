using Blazor_App.Shared.DTOs;

namespace Blazor_App.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task PlaceOrder();
        Task<List<OrderOverviewResponseDto>> GetOrders();
        Task<OrderDetailsResponseDto> GetOrderDetails(int orderId);
    }
}
