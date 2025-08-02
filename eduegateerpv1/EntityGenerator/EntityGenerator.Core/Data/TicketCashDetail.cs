using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TicketCashDetails", Schema = "cs")]
    public partial class TicketCashDetail
    {
        [Key]
        public long CashDetailID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal RefundAmount { get; set; }
        public long ReferenceDetailID { get; set; }
    }
}
