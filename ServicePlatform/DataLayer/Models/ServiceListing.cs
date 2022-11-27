using ServicePlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlatform.DataLayer.Models
{
    public class ServiceListing
    {
        [Key]
        public int ServiceListingId { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateApproved { get; set; }
        public decimal AssessmentFee { get; set; }
        public AdminApprovalStatus ListingApprovalStatus { get; set; }


        //Navigation properties
        [ForeignKey("Vendor")]
        public int VendorId { get; set; }

        [ForeignKey("ServiceId")]
        public int ServiceId { get; set; }
    }
}
