using ServicePlatform.Utility.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlatform.DataLayer.Models
{
    public class Assessment
    {
        [Key]
        public int AssessmentId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CompletionTime { get; set; }
        public ServiceStatus AssessmentStatus { get; set; }
        public string Details { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        ////Navigation
        //[ForeignKey("Customer")]
        //public int CustomerId { get; set; }
        
        //[ForeignKey("ServiceListing")]
        //public int ServiceListingId { get; set; }
    }
}
