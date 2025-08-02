using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderMasterAddressLog
    {
        [Key]
        public int OrderMasterAddressLogID { get; set; }
        public long RefOrderID { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}
