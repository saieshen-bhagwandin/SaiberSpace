using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class UserLoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;


        public string Name { get; set; } = string.Empty;


        public string Surname { get; set; } = string.Empty;

  
        public string StreetAddress { get; set; } = string.Empty;

        public string Suburb { get; set; } = string.Empty;

    }
}
