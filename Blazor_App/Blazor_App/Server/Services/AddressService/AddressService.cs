using Blazor_App.Server.Data;
using Blazor_App.Server.Services.AuthService;
using Blazor_App.Shared.Models;

namespace Blazor_App.Server.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;

        public AddressService(AppDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<Address>> AddOrUpdateAddress(Address address)
        {
            var response = new ServiceResponse<Address>();
            var dbAddress = (await GetAddress()).Data;
            if(dbAddress == null)
            {
                address.UserId = _authService.GetUserId();
                _context.Addresses.Add(address);
                response.Data = address;
            }
            else
            {
                dbAddress.FirstName = address.FirstName;
                dbAddress.LastName = address.LastName;
                dbAddress.Street = address.Street;
                dbAddress.City = address.City;
                dbAddress.State = address.State;
                dbAddress.Country = address.Country;
                dbAddress.Zip = address.Zip;
                response.Data = dbAddress;
            }

            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<ServiceResponse<Address>> GetAddress()
        {
            int userId = _authService.GetUserId();
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.UserId == userId);
            return new ServiceResponse<Address> { Data = address };
        }
    }
}
