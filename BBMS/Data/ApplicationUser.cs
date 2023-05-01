using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime? DataOfBirth { get; set; }
    }
}
