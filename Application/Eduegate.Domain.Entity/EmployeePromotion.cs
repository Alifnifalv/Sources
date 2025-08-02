namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.EmployeePromotions")]
    public partial class EmployeePromotion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeePromotion()
        {
            EmployeePromotionLeaveAllocations = new HashSet<EmployeePromotionLeaveAllocation>();
            EmployeePromotionSalaryComponentMaps = new HashSet<EmployeePromotionSalaryComponentMap>();
        }

        [Key]
        public long EmployeePromotionIID { get; set; }

        public long? EmployeeID { get; set; }

        public long? SalaryStructureID { get; set; }

        public DateTime? FromDate { get; set; }

        public decimal? Amount { get; set; }

        public byte? PayrollFrequencyID { get; set; }

        public bool? IsSalaryBasedOnTimeSheet { get; set; }

        public int? TimeSheetSalaryComponentID { get; set; }

        public decimal? TimeSheetHourRate { get; set; }

        public decimal? TimeSheetLeaveEncashmentPerDay { get; set; }

        public decimal? TimeSheetMaximumBenefits { get; set; }

        public int? PaymentModeID { get; set; }

        public long? AccountID { get; set; }

        public int? OldRoleID { get; set; }

        public int? OldDesignationID { get; set; }

        public long? OldBranchID { get; set; }

        public int? NewRoleID { get; set; }

        public int? NewDesignationID { get; set; }

        public long? NewBranchID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? OldDepartmentID { get; set; }

        public int? NewLeaveGroupID { get; set; }

        public int? OldLeaveGroupID { get; set; }

        public long? OldSalaryStructureID { get; set; }

        public long? NewSalaryStructureID { get; set; }

        public bool? IsApplyImmediately { get; set; }

        public virtual Account Account { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Branch Branch1 { get; set; }

        public virtual Designation Designation { get; set; }

        public virtual Designation Designation1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotionLeaveAllocation> EmployeePromotionLeaveAllocations { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual LeaveGroup LeaveGroup { get; set; }

        public virtual EmployeeRole EmployeeRole { get; set; }

        public virtual SalaryStructure SalaryStructure { get; set; }

        public virtual LeaveGroup LeaveGroup1 { get; set; }

        public virtual EmployeeRole EmployeeRole1 { get; set; }

        public virtual SalaryStructure SalaryStructure1 { get; set; }

        public virtual PayrollFrequency PayrollFrequency { get; set; }

        public virtual SalaryComponent SalaryComponent { get; set; }

        public virtual SalaryPaymentMode SalaryPaymentMode { get; set; }

        public virtual SalaryStructure SalaryStructure2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotionSalaryComponentMap> EmployeePromotionSalaryComponentMaps { get; set; }
    }
}
