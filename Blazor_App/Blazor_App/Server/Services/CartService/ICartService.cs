﻿using Blazor_App.Shared.DTOs;
using Blazor_App.Shared.Models;

namespace Blazor_App.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductResponseDto>>> GetCartProducts(List<CartItem> cartItems);
        Task<ServiceResponse<List<CartProductResponseDto>>> StoreCartItems(List<CartItem> cartItems);
    }
}
