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
        public string MiddleName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string State { get; set; }

       // [Required]

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string BusinessName { get; set; }

     
        public string BusinessDescription { get; set; }

        public string Expertise { get; set; } 
    }
}
