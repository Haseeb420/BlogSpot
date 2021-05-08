using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlogSpot.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public int Rank { get; set; }

        [Required(ErrorMessage = "Name can't be Empty")]
        [StringLength(50, ErrorMessage = "Name can't be more 50 characters")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of Birth can't be Empty")]
        [DataType(DataType.Date)]
        public DateTime DateOfBrith { get; set; }

        [Required(ErrorMessage = "Email can't be Empty.....!")]
        [StringLength(30, ErrorMessage = "Name can't be more 30 characters.....!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Plese Enter email in right format....!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Select your Gender.....!")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "password can't be Empty.....!")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="Password should be greater then 6 characters.....!")]
        [MaxLength(16,ErrorMessage ="Password can't be greater then 16 characters.....!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password can't be Empty.....!")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password should be greater then 6 characters.....!")]
        [MaxLength(16, ErrorMessage = "Password can't be greater then 16 characters.....!")]
        [Compare("Password", ErrorMessage ="Please confirm your Password")]
        public string ConfirmPassword { get; set; }
    }
}
