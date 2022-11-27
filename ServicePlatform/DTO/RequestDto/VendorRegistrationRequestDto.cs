using System.ComponentModel.DataAnnotations;

namespace ServicePlatform.DTO.RequestDto
{
    public class VendorRegistrationRequestDto : CustomerRegistrationRequestDto
    {
        [Required]
        public string Location { get; set; }

        [Required]
        public string Expertise { get; set; }
    }
}
