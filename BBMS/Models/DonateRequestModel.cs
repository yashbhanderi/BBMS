using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Models
{
    public class DonateRequestModel
    {
        public string UserId { get; set; }
        public int Id { get; set; }

        [Required, Display(Name = "Donor Name")]
        public string DonorName { get; set; }

        [Required, Display(Name = "Donor Age")]
        public int DonorAge { get; set; }

        [Required]
        public string Disease { get; set; }

        [Required, Display(Name = "BloodGroup")]
        public int BloodGroupId { get; set; }
        public string BloodGroup { get; set; }

        [Required, Display(Name = "Unit")]
        public int Unit { get; set; }
        public string RequestStatus { get; set; }

        [Required, Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; }
    }
}
