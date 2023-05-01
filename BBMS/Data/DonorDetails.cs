using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Data
{
    public class DonorDetails
    {
        public int Id { get; set; }

        public string DonorName { get; set; }

        public int DonorAge { get; set; }

        public string Disease { get; set; }

        public int BloodGroupId { get; set; }
        public int Unit { get; set; }

        public DateTime RequestDate { get; set; }
        public BloodStock BloodStock { get; set; }

        public string UserId { get; set; }
        public Status RequestStatus { get; set; }
    }
    public enum Status
    {
        Accepted,
        Rejected,
        Pending
    }
}
