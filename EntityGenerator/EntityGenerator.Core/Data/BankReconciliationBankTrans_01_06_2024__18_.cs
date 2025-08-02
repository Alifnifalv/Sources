using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("BankReconciliationBankTrans_01_06_2024 (18)")]
    public partial class BankReconciliationBankTrans_01_06_2024__18_
    {
        [Column(TypeName = "date")]
        public DateTime PostDate { get; set; }
        [StringLength(500)]
        public string PartyName { get; set; }
        [StringLength(500)]
        public string Reference { get; set; }
        [StringLength(50)]
        public string ChequeNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ChequeDate { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public double? Debit { get; set; }
        public double? Credit { get; set; }
        public double Balance { get; set; }
    }
}
