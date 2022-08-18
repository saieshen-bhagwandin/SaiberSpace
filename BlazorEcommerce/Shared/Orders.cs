using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class Orders
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ProductIds { get; set; } = string.Empty;

        public string Quantity { get; set; } = string.Empty;
        public DateTime Date { get; set; }

    }
}
