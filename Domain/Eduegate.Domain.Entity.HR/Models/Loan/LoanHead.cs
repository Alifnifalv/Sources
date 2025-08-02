using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
    using Eduegate.Domain.Entity.HR.Models;
namespace Eduegate.Domain.Entity.HR.Loan
{
    [Table("LoanHead", Schema = "payroll")]
    public partial class LoanHead
    {
        public LoanHead()
        {
            LoanDetails = new HashSet<LoanDetail>();
        }

        [Key]
        public long LoanHeadIID { get; set; }
        public long? LoanRequestID { get; set; }
        public long? EmployeeID { get; set; }
        public byte? LoanTypeID { get; set; }
        [StringLength(25)]
        public string LoanNo { get; set; }
        public byte? LoanStatusID { get; set; }
        public short? NoOfInstallments { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LoanDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PaymentStartDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LoanAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? InstallmentAmount { get; set; }
        public string Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
       
        [Column(TypeName = "datetime")]
        public DateTime? LastInstallmentDate { get; set; }
       
        public decimal? LastInstallmentAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PaymentEndDate { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("LoanHeads")]
        public virtual Employee Employee { get; set; }
    
        public virtual LoanStatus LoanStatus { get; set; }
       
        public virtual LoanType LoanType { get; set; }
        public virtual LoanRequest LoanRequest { get; set; }
        [InverseProperty("LoanHead")]
        public virtual ICollection<LoanDetail> LoanDetails { get; set; }
    }
}
