using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
