using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.HR.Models;
namespace Eduegate.Domain.Entity.HR.Loan
{
    [Table("LoanRequests", Schema = "payroll")]
    public partial class LoanRequest
    {
        public LoanRequest()
        {
            LoanHeads = new HashSet<LoanHead>();
        }
        [Key]
        public long LoanRequestIID { get; set; }
        public long? EmployeeID { get; set; }
        public byte? LoanTypeID { get; set; }
        [StringLength(25)]
        public string LoanRequestNo { get; set; }
        public byte? LoanRequestStatusID { get; set; }
        public short? NoOfInstallments { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LoanRequestDate { get; set; }
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
        //public byte[] TimeStamps { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("LoanRequests")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("LoanRequestStatusID")]
        [InverseProperty("LoanRequests")]
        public virtual LoanRequestStatus LoanRequestStatus { get; set; }
        [ForeignKey("LoanTypeID")]
        [InverseProperty("LoanRequests")]
        public virtual LoanType LoanType { get; set; }
        public virtual ICollection<LoanHead> LoanHeads { get; set; }
    }
}
