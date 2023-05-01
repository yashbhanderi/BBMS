using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Please enter First number")]
        [Display(Name = "First Name")]
        public string Fname { get; set; }

        [Required(ErrorMessage = "Please enter Last number")]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }


        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address")]
        public string Email { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Please enter Passoword")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [Compare("Password", ErrorMessage = "Password does not match.")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
