using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class EmailDTO
    {

        public UserRegisterRequest user { get; set; }
        public List<CartItem> cartItem { get; set; }

        public string Ordernumber { get; set; } = String.Empty;


    }
}
