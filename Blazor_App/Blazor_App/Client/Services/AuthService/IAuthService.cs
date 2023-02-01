using Blazor_App.Shared.VM;

namespace Blazor_App.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
    }
}
