using Blazor_App.Server.Services.OrderService;
using Blazor_App.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blazor_App.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //[HttpPost]
        //public async Task<ActionResult<ServiceResponse<bool>>> PlaceOrder()
        //{
        //    var result = await _orderService.PlaceOrder();
        //    return Ok(result);
        //}

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<OrderOverviewResponseDto>>>> GetOrders()
        {
            var result = await _orderService.GetOrders();
            return Ok(result);
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<ActionResult<ServiceResponse<OrderDetailsResponseDto>>> GetOrderDetails(int orderId)
        {
            var result = await _orderService.GetOrderDetails(orderId);
            return Ok(result);
        }

    }
}
