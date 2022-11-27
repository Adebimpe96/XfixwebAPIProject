using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.DTO.ResponseDto
{
    public class VendorRegistrationResponseDto
    {
        [Required]
        public string Expertise { get; set; }
    }
}
