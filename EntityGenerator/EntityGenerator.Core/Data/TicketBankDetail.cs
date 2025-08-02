using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TicketBankDetails", Schema = "cs")]
    public partial class TicketBankDetail
    {
        [Key]
        public long BankIID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string BankName { get; set; }
        [StringLength(100)]
        public string AccountNumber { get; set; }
        public long ReferenceDetailID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal RefundAmount { get; set; }
    }
}
