using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CostCenters", Schema = "account")]
    public partial class CostCenter
    {
        public CostCenter()
        {
            AccountTransactions = new HashSet<AccountTransaction>();
            BudgetEntryAccountMaps = new HashSet<BudgetEntryAccountMap>();
            BudgetEntryCostCenterMaps = new HashSet<BudgetEntryCostCenterMap>();
            Classes = new HashSet<Class>();
            Route1 = new HashSet<Route1>();
            TransactionDetails = new HashSet<TransactionDetail>();
        }

        [Key]
        public int CostCenterID { get; set; }
        [StringLength(50)]
        public string CostCenterCode { get; set; }
        [StringLength(100)]
        public string CostCenterName { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsAffect_A { get; set; }
        public bool? IsAffect_L { get; set; }
        public bool? IsAffect_C { get; set; }
        public bool? IsAffect_E { get; set; }
        public bool? IsAffect_I { get; set; }
        public bool? IsFixed { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("CostCenters")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("CostCenters")]
        public virtual School School { get; set; }
        [InverseProperty("CostCenter")]
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
        [InverseProperty("CostCenter")]
        public virtual ICollection<BudgetEntryAccountMap> BudgetEntryAccountMaps { get; set; }
        [InverseProperty("CostCenter")]
        public virtual ICollection<BudgetEntryCostCenterMap> BudgetEntryCostCenterMaps { get; set; }
        [InverseProperty("CostCenter")]
        public virtual ICollection<Class> Classes { get; set; }
        [InverseProperty("CostCenter")]
        public virtual ICollection<Route1> Route1 { get; set; }
        [InverseProperty("CostCenter")]
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
