using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int EditionId { get; set; }
        public string ProductTitle { get; set; } = String.Empty;
        public string EditionName { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = String.Empty;
        public int Quantity { get; set; }
    }
}
