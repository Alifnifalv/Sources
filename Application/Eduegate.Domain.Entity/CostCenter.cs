namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.CostCenters")]
    public partial class CostCenter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CostCenter()
        {
            AccountTransactions = new HashSet<AccountTransaction>();
            Classes = new HashSet<Class>();
            CostCenterAccountMaps = new HashSet<CostCenterAccountMap>();
            DepartmentCostCenterMaps = new HashSet<DepartmentCostCenterMap>();
            Routes1 = new HashSet<Routes1>();
            TransactionDetails = new HashSet<TransactionDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CostCenterID { get; set; }

        [StringLength(50)]
        public string CostCenterCode { get; set; }

        [StringLength(100)]
        public string CostCenterName { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Class> Classes { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual School School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CostCenterAccountMap> CostCenterAccountMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepartmentCostCenterMap> DepartmentCostCenterMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Routes1> Routes1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
