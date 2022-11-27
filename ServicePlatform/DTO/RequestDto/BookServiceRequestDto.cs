using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.DTO.RequestDto
{
    public class BookServiceRequestDto
    {
        [Required]
        public string CustomerName { get; set; }
        
        [Required]
        public string ContactNumber { get; set; }
        
        [Required]
        public string Address { get; set; }
    }
}
