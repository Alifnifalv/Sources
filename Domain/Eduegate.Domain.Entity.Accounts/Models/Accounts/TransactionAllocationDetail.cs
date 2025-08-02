using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
{
    [Table("TransactionAllocationDetails", Schema = "account")]
    public partial class TransactionAllocationDetail
    {

        [Key]
        public long TransactionAllocationDetailIID { get; set; }
        public long TransactionAllocationID { get; set; }
        public long? AccountTransactionHeadID { get; set; }
        public long? Ref_TH_ID { get; set; }
        public int? Ref_SlNo { get; set; }
        public int? CostCenterID { get; set; }
        public int? DepartmentID { get; set; }
        public int? BudgetID { get; set; }
        public long? AccountID { get; set; }
        public long? GL_AccountID { get; set; }
        public long? SL_AccountID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount { get; set; }
        [StringLength(1000)]
        public string Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("TransactionAllocationDetailAccounts")]
        public virtual Account Account { get; set; }
        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("TransactionAllocationDetails")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        [ForeignKey("CostCenterID")]
        [InverseProperty("TransactionAllocationDetails")]
        public virtual CostCenter CostCenter { get; set; }
        [ForeignKey("GL_AccountID")]
        [InverseProperty("TransactionAllocationDetailGL_Account")]
        public virtual Account GL_Account { get; set; }
        [ForeignKey("TransactionAllocationID")]
        [InverseProperty("TransactionAllocationDetails")]
        public virtual TransactionAllocationHead TransactionAllocation { get; set; }
    }
}
