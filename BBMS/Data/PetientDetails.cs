using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Data
{
    public class PetientDetails
    {
        public int Id { get; set; }

        public string PatientName { get; set; }

        public int PatientAge { get; set; }

        public string Disease { get; set; }

        public int BloodGroupId { get; set; }
        public int Unit { get; set; }
        public string UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public BloodStock BloodStock { get; set; }

        public RequestStatus RequestStatus { get; set; }

    }
    public enum RequestStatus
    {
        Accepted,
        Rejected,
        Pending
    }
}
