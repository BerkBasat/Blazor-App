using Blazor_App.Shared.Models;

namespace Blazor_App.Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetCategories();
    }
}
