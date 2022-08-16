using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class Edition
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;    

    }
}
