using Blazor_App.Shared.Models;

namespace Blazor_App.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        List<Category> Categories { get; set; }
        Task GetCategories();
    }
}
