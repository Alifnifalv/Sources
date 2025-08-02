using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VoucherValidityLog
    {
        [Key]
        public int VoucherValidityLogID { get; set; }
        public int RefVoucherID { get; set; }
        public short ValidDays { get; set; }
        public string Remarks { get; set; }
        public short CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
