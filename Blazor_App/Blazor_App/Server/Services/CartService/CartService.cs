using Blazor_App.Server.Services.AuthService;
using Blazor_App.Server.Data;
using Blazor_App.Server.Migrations;
using Blazor_App.Shared.DTOs;
using Blazor_App.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CartItem = Blazor_App.Shared.Models.CartItem;

namespace Blazor_App.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;

        public CartService(AppDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }


        public async Task<ServiceResponse<List<CartProductResponseDto>>> GetCartProducts(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponseDto>>
            {
                Data = new List<CartProductResponseDto>()
            };

            foreach (var item in cartItems)
            {
                var product = await _context.Products
                    .Where(p => p.Id == item.ProductId)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    continue;
                }

                var productVariant = await _context.ProductVariants
                    .Where(v => v.ProductId == item.ProductId
                    && v.ProductTypeId == item.ProductTypeId)
                    .Include(v => v.ProductType)
                    .FirstOrDefaultAsync();

                if (productVariant == null)
                {
                    continue;
                }

                var cartProduct = new CartProductResponseDto
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductType = productVariant.ProductType.Name,
                    ProductTypeId = productVariant.ProductTypeId,
                    Quantity = item.Quantity

                };

                result.Data.Add(cartProduct);
            }

            return result;
        }

        public async Task<ServiceResponse<List<CartProductResponseDto>>> StoreCartItems(List<CartItem> cartItems)
        {
            cartItems.ForEach(item => item.UserId = _authService.GetUserId());
            _context.CartItems.AddRange(cartItems);
            await _context.SaveChangesAsync();

            return await GetDbCartProducts();
        }

        public async Task<ServiceResponse<int>> GetCartItemsCount()
        {
            var count = (await _context.CartItems.Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync()).Count();

            return new ServiceResponse<int>
            {
                Data = count
            };
        }

        public async Task<ServiceResponse<List<CartProductResponseDto>>> GetDbCartProducts(int? userId = null)
        {
            if (userId == null)
                userId = _authService.GetUserId();
                

            return await GetCartProducts(await _context.CartItems.Where(ci => ci.UserId == userId).ToListAsync());
        }

        public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
        {
            cartItem.UserId = _authService.GetUserId();

            var sameItem = await _context.CartItems.
                FirstOrDefaultAsync(ci => 
                ci.ProductId == cartItem.ProductId && 
                ci.ProductId == cartItem.ProductTypeId &&
                ci.UserId == cartItem.UserId);
            if (sameItem == null)
            {
                _context.CartItems.Add(cartItem);
            }
            else
            {
                sameItem.Quantity += cartItem.Quantity;
            }

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true
            };
        }

        public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
        {

            //TODO: Update and delete returns null(false) for some of the products but works fine on others???
            var dbCartItem = await _context.CartItems.
                FirstOrDefaultAsync(ci =>
                ci.ProductId == cartItem.ProductId &&
                ci.ProductId == cartItem.ProductTypeId &&
                ci.UserId == _authService.GetUserId());

            if(dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Item does not exist!"
                };
            }

            dbCartItem.Quantity = cartItem.Quantity;
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true
            };
        }

        public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId)
        {
            var dbCartItem = await _context.CartItems.
                FirstOrDefaultAsync(ci =>
                ci.ProductId == productId &&
                ci.ProductId == productTypeId &&
                ci.UserId == _authService.GetUserId());

            if (dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Item does not exist!"
                };
            }

            _context.CartItems.Remove(dbCartItem);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true
            };
        }
    }
}
