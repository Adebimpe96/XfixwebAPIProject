using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.DTO.RequestDto
{
    public class CreateServiceListingRequestDto
    {
        [Required]
        public string ListingName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public decimal AssessmentFee { get; set; }
    }
}
