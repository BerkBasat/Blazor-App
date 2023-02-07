using Blazor_App.Shared.DTOs;

namespace Blazor_App.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrder();
        Task<ServiceResponse<List<OrderOverviewResponseDto>>> GetOrders();
    }
}
