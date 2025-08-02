using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LoanDetail", Schema = "payroll")]
    public partial class LoanDetail
    {
        [Key]
        public long LoanDetailID { get; set; }
        public long? LoanHeadID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InstallmentDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InstallmentReceivedDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? InstallmentAmount { get; set; }
        public string Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsPaid { get; set; }
        public byte? LoanEntryStatusID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PaidAmount { get; set; }

        [ForeignKey("LoanEntryStatusID")]
        [InverseProperty("LoanDetails")]
        public virtual LoanEntryStatus LoanEntryStatus { get; set; }
        [ForeignKey("LoanHeadID")]
        [InverseProperty("LoanDetails")]
        public virtual LoanHead LoanHead { get; set; }
    }
}
