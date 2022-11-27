using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.DataLayer.Models
{
    public class AccountUser : BaseEntity
    {
        [Key]
        public int UserId { get; set; }

        public string BusinessName { get; set; }
        public string Website { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessDescription { get; set; }
        public string Expertise { get; set; }

    }
}
