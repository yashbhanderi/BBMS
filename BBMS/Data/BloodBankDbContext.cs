using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Data
{
    public class BloodBankDbContext : IdentityDbContext<ApplicationUser>
    {
        public BloodBankDbContext(DbContextOptions<BloodBankDbContext> options)
            :base(options)
        {

        }
        public DbSet<PetientDetails> PetientDetails { get; set; }

        public DbSet<BloodStock> BloodStock { get; set; }

        public DbSet<DonorDetails> DonorDetails { get; set; }
    }
}
