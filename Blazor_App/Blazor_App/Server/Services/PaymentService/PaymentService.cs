﻿using Blazor_App.Server.Services.AuthService;
using Blazor_App.Server.Services.CartService;
using Blazor_App.Server.Services.OrderService;
using Stripe;
using Stripe.Checkout;

namespace Blazor_App.Server.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;

        public PaymentService(ICartService cartService, IAuthService authService, IOrderService orderService)
        {
            StripeConfiguration.ApiKey = "sk_test_51MYpZuDLDtHSJHPC2k3lHgiHeBvCqYN0EMirZWELowm9qn6Sq9HmCbClN3VlhoBaPWLKb3Y3Qes5ma0kqXZlMlsr004rcvhoT5";

            _cartService = cartService;
            _authService = authService;
            _orderService = orderService;
        }


        public async Task<Session> CreateCheckoutSession()
        {
            var products = (await _cartService.GetDbCartProducts()).Data;
            var lineItems = new List<SessionLineItemOptions>();
            products.ForEach(product => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = product.Price * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Title,
                        Images = new List<string> { product.ImageUrl}
                    }
                },
                Quantity = product.Quantity
            }));

            var options = new SessionCreateOptions
            {
                CustomerEmail = _authService.GetUserEmail(),
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7038/order-success",
                CancelUrl = "https://localhost:7038/cart"
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return session;
        }
    }
}
