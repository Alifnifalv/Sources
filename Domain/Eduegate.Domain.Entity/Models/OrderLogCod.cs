using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderLogCod
    {
        [Key]
        public int LogID { get; set; }
        public long RefOrderID { get; set; }
        public long RefCustomerID { get; set; }
        public long RefAdminID { get; set; }
        public System.DateTime LogOn { get; set; }
    }
}
