using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeMasters", Schema = "schools")]
    public partial class FeeMaster
    {
        public FeeMaster()
        {
            BudgetEntryFeeMaps = new HashSet<BudgetEntryFeeMap>();
            CampusTransferFeeTypeMaps = new HashSet<CampusTransferFeeTypeMap>();
            CreditNoteFeeTypeMaps = new HashSet<CreditNoteFeeTypeMap>();
            FeeCollectionFeeTypeMaps = new HashSet<FeeCollectionFeeTypeMap>();
            FeeCollections = new HashSet<FeeCollection>();
            FeeDueFeeTypeMaps = new HashSet<FeeDueFeeTypeMap>();
            FeeDueInventoryMaps = new HashSet<FeeDueInventoryMap>();
            FeeMasterClassMaps = new HashSet<FeeMasterClassMap>();
            FeeStructureFeeMaps = new HashSet<FeeStructureFeeMap>();
            FinalSettlementFeeTypeMaps = new HashSet<FinalSettlementFeeTypeMap>();
            ProductStudentMaps = new HashSet<ProductStudentMap>();
            RefundFeeTypeMaps = new HashSet<RefundFeeTypeMap>();
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
            StudentGroupFeeTypeMaps = new HashSet<StudentGroupFeeTypeMap>();
        }

        [Key]
        public int FeeMasterID { get; set; }
        [StringLength(50)]
        public string FeeCode { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int? FeeTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? FeeCycleID { get; set; }
        public long? LedgerAccountID { get; set; }
        public long? TaxLedgerAccountID { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public decimal? TaxPercentage { get; set; }
        public int? DueInDays { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public long? AdvanceAccountID { get; set; }
        public long? OutstandingAccountID { get; set; }
        public long? OSTaxAccountID { get; set; }
        public long? AdvanceTaxAccountID { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public decimal? OSTaxPercentage { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public decimal? AdvanceTaxPercentage { get; set; }
        public long? ProvisionforAdvanceAccountID { get; set; }
        public long? ProvisionforOutstandingAccountID { get; set; }
        public bool? IsExternal { get; set; }
        public string ReportName { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("FeeMasters")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("AdvanceAccountID")]
        [InverseProperty("FeeMasterAdvanceAccounts")]
        public virtual Account AdvanceAccount { get; set; }
        [ForeignKey("AdvanceTaxAccountID")]
        [InverseProperty("FeeMasterAdvanceTaxAccounts")]
        public virtual Account AdvanceTaxAccount { get; set; }
        [ForeignKey("FeeCycleID")]
        [InverseProperty("FeeMasters")]
        public virtual FeeCycle FeeCycle { get; set; }
        [ForeignKey("FeeTypeID")]
        [InverseProperty("FeeMasters")]
        public virtual FeeType FeeType { get; set; }
        [ForeignKey("LedgerAccountID")]
        [InverseProperty("FeeMasterLedgerAccounts")]
        public virtual Account LedgerAccount { get; set; }
        [ForeignKey("OSTaxAccountID")]
        [InverseProperty("FeeMasterOSTaxAccounts")]
        public virtual Account OSTaxAccount { get; set; }
        [ForeignKey("OutstandingAccountID")]
        [InverseProperty("FeeMasterOutstandingAccounts")]
        public virtual Account OutstandingAccount { get; set; }
        [ForeignKey("ProvisionforAdvanceAccountID")]
        [InverseProperty("FeeMasterProvisionforAdvanceAccounts")]
        public virtual Account ProvisionforAdvanceAccount { get; set; }
        [ForeignKey("ProvisionforOutstandingAccountID")]
        [InverseProperty("FeeMasterProvisionforOutstandingAccounts")]
        public virtual Account ProvisionforOutstandingAccount { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("FeeMasters")]
        public virtual School School { get; set; }
        [ForeignKey("TaxLedgerAccountID")]
        [InverseProperty("FeeMasterTaxLedgerAccounts")]
        public virtual Account TaxLedgerAccount { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<BudgetEntryFeeMap> BudgetEntryFeeMaps { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<CampusTransferFeeTypeMap> CampusTransferFeeTypeMaps { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<FeeCollection> FeeCollections { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<FeeDueInventoryMap> FeeDueInventoryMaps { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<FeeMasterClassMap> FeeMasterClassMaps { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<FeeStructureFeeMap> FeeStructureFeeMaps { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<FinalSettlementFeeTypeMap> FinalSettlementFeeTypeMaps { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<ProductStudentMap> ProductStudentMaps { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<RefundFeeTypeMap> RefundFeeTypeMaps { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }
        [InverseProperty("FeeMaster")]
        public virtual ICollection<StudentGroupFeeTypeMap> StudentGroupFeeTypeMaps { get; set; }
    }
}
