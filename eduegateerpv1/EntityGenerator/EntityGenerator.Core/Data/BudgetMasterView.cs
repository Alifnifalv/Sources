using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BudgetMasterView
    {
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
        [StringLength(100)]
        public string Currency { get; set; }
        [StringLength(50)]
        public string Department { get; set; }
        [StringLength(50)]
        public string BudgetStatus { get; set; }
    }
}
