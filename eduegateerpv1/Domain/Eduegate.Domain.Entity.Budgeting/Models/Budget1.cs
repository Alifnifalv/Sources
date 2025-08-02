using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Budgeting.Models
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

        public DateTime? PeriodStart { get; set; }

        public DateTime? PeriodEnd { get; set; }

        public byte? BudgetStatusID { get; set; }

        public long? DepartmentID { get; set; }

        public int? CurrencyID { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        public decimal? BudgetTotalValue { get; set; }

        public byte? BudgetGroupID { get; set; }

        public byte? BudgetTypeID { get; set; }

        public int? CompanyID { get; set; }

        public int? FinancialYearID { get; set; }

        public virtual BudgetGroup BudgetGroup { get; set; }
      
        public virtual BudgetType BudgetType { get; set; }

        public virtual BudgetStatus BudgetStatus { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Company Company { get; set; }

        public virtual FiscalYear FinancialYear { get; set; }

        public virtual Departments1 Department { get; set; }

        public virtual ICollection<BudgetEntry> BudgetEntries { get; set; }
       
        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}