using BBMS.Data;
using BBMS.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Areas.Admin.Repository
{

    public class AdminBloodBankRepository
    {
        private readonly BloodBankDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminBloodBankRepository(BloodBankDbContext context, IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userService = userService;
            _userManager = userManager;
        }
        
    }
}
