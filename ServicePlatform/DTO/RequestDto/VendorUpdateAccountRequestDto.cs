using ServicePlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.DTO.RequestDto
{
    public class VendorUpdateAccountRequestDto
    {
        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string BusinessName { get; set; }

        [Required]
        public string Website { get; set; }

        [Required]
        public string BusinessEmail { get; set; }

        [Required]
        public string BusinessDescription { get; set; }
    }
}
