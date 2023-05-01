using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Models
{
    public class PetientRequestModel
    {
        public string UserId { get; set; }
        public int Id { get; set; }

        [Required, Display(Name ="Petient Name")]
        public string PetientName { get; set; }

        [Required, Display(Name = "Petient Age")]
        public int PetientAge { get; set; }

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
