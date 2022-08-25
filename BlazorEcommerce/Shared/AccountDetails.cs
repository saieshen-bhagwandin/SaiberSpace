using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class AccountDetails
    {

        public UserRegisterRequest user { get; set; }

        public List<Orders> Orders { get; set; }

    


    }
}
