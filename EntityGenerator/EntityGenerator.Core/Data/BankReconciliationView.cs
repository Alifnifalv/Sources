using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BankReconciliationView
    {
        public long BankReconciliationHeadIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? OpeningBalanceAccount { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? ClosingBalanceAccount { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? OpeningBalanceBankStatement { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? ClosingBalanceBankStatement { get; set; }
        [Required]
        [StringLength(250)]
        public string BankReconciliationStatus { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [StringLength(250)]
        public string FileName { get; set; }
    }
}
