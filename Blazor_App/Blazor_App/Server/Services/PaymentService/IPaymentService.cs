using Stripe.Checkout;

namespace Blazor_App.Server.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<Session> CreateCheckoutSession();
    }
}
