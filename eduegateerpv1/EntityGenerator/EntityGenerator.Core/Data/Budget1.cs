using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Budgets", Schema = "budget")]
    public partial class Budget1
    {
        public Budget1()
        {
            BudgetEntries = new HashSet<BudgetEntry>();
        }

        [Key]
        public int BudgetID { get; set; }
        [StringLength(50)]
        public string BudgetCode { get; set; }
        [StringLength(100)]
        public string BudgetName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PeriodStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PeriodEnd { get; set; }
        public byte? BudgetStatusID { get; set; }
        public long? DepartmentID { get; set; }
        public int? CurrencyID { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BudgetTotalValue { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? BudgetGroupID { get; set; }
        public byte? BudgetTypeID { get; set; }
        public int? CompanyID { get; set; }
        public int? FinancialYearID { get; set; }

        [ForeignKey("BudgetGroupID")]
        [InverseProperty("Budget1")]
        public virtual BudgetGroup BudgetGroup { get; set; }
        [ForeignKey("BudgetStatusID")]
        [InverseProperty("Budget1")]
        public virtual BudgetStatus BudgetStatus { get; set; }
        [ForeignKey("BudgetTypeID")]
        [InverseProperty("Budget1")]
        public virtual BudgetType BudgetType { get; set; }
        [ForeignKey("CompanyID")]
        [InverseProperty("Budget1")]
        public virtual Company Company { get; set; }
        [ForeignKey("CurrencyID")]
        [InverseProperty("Budget1")]
        public virtual Currency Currency { get; set; }
        [ForeignKey("DepartmentID")]
        [InverseProperty("Budget1")]
        public virtual Department1 Department { get; set; }
        [ForeignKey("FinancialYearID")]
        [InverseProperty("Budget1")]
        public virtual FiscalYear FinancialYear { get; set; }
        [InverseProperty("Budget")]
        public virtual ICollection<BudgetEntry> BudgetEntries { get; set; }
    }
}
