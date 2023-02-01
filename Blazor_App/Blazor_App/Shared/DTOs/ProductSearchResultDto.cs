using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor_App.Shared.Models;

namespace Blazor_App.Shared.DTOs
{
    public class ProductSearchResultDto
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
