using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlogSpot.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email field is required....!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Coorect Email....!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password field is required....!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
