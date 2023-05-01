using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Data
{
    public class BloodStock
    {
        public int Id { get; set; }

        public string BloodGroup { get; set; }

        public int Units { get; set; }

        public ICollection<PetientDetails> PetientDetails { get; set; }

        public ICollection<DonorDetails> DonorDetails { get; set; }
    }
}
