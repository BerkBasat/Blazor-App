using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_App.Shared.Models
{
    // This will be used to introduce new variants to products. For example a book will also have an audiobook or e-book version.
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
