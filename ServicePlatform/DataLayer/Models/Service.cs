using ServicePlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlatform.DataLayer.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }
        public string ListingName { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOfCompletion { get; set; }
        public ServiceStatus BookServiceStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        //Navigation properties
        [ForeignKey("ServiceLisitingId")]
        public int ServiceLisitingId { get; set; }

        [ForeignKey("AssessmentId")]
        public int AssessmentId { get; set; }
       
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
    }
}



