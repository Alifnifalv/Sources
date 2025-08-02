using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerLog
    {
        [Key]
        public long RefCustomerID { get; set; }
        public System.DateTime ActionTime { get; set; }
        public string ActionLog { get; set; }
        public string IpAddress { get; set; }
        public string IpCountry { get; set; }
        public string ActionKey { get; set; }
        public virtual CustomerMaster CustomerMaster { get; set; }
    }
}
