using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class UserRegisterRequest
    {

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;


        [Display(Name = "Name")]
        [Required, RegularExpression("[a-zA-Z]+", ErrorMessage = "Name is invalid")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Surname")]
        [Required, RegularExpression("[a-zA-Z]+",ErrorMessage = "Surname is invalid")]
        public string Surname { get; set; } = string.Empty;

        [Required]
        public string StreetAddress { get; set; } = string.Empty;

        public string Suburb { get; set; } = string.Empty;


    }
}
