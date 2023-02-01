using Blazor_App.Shared.Models;

namespace Blazor_App.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        public Task<ServiceResponse<int>> Register(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExists(string email)
        {
            throw new NotImplementedException();
        }
    }
}
