using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("PaymentModes_20211221", Schema = "account")]
    public partial class PaymentModes_20211221
    {
        public int PaymentModeID { get; set; }
        [StringLength(50)]
        public string PaymentModeName { get; set; }
        public long? AccountId { get; set; }
        public int? TenderTypeID { get; set; }
    }
}
