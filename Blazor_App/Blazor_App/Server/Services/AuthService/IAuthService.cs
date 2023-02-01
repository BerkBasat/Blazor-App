﻿using Blazor_App.Shared.Models;

namespace Blazor_App.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExists(string email);
    }
}