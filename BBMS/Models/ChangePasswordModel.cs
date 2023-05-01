using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Please Enter Old Password"), Display(Name = "Old Password"), DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password"), Display(Name = "New Password"), DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, Display(Name = "Confirm New Password"), DataType(DataType.Password), Compare("NewPassword", ErrorMessage = "Password Not Match")]
        public string ConfirmPassword { get; set; }
    }
}
