using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;

namespace Eduegate.Domain.Entity.Accounts.Models.Schools
{
    [Table("FeeMasters", Schema = "schools")]
    public partial class FeeMaster
    {
        public FeeMaster()
        {
        }

        [Key]
        public int FeeMasterID { get; set; }

        [StringLength(50)]
        public string FeeCode { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int? FeeTypeID { get; set; }

        public DateTime? DueDate { get; set; }

        public decimal? Amount { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public byte? FeeCycleID { get; set; }

        public long? LedgerAccountID { get; set; }

        public long? TaxLedgerAccountID { get; set; }

        public decimal? TaxPercentage { get; set; }

        public int? DueInDays { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public long? AdvanceAccountID { get; set; }

        public long? OutstandingAccountID { get; set; }

        public long? OSTaxAccountID { get; set; }

        public long? AdvanceTaxAccountID { get; set; }

        public decimal? OSTaxPercentage { get; set; }

        public decimal? AdvanceTaxPercentage { get; set; }

        public long? ProvisionforAdvanceAccountID { get; set; }

        public long? ProvisionforOutstandingAccountID { get; set; }

        public bool? IsExternal { get; set; }

        public string ReportName { get; set; }

        public bool? IsActive { get; set; }

        //public virtual AcademicYear AcademicYear { get; set; }

        public virtual Account AdvanceAccount { get; set; }

        public virtual Account AdvanceTaxAccount { get; set; }

        public virtual Account LedgerAccount { get; set; }

        public virtual Account OSTaxAccount { get; set; }

        public virtual Account OutstandingAccount { get; set; }

        public virtual Account ProvisionforAdvanceAccount { get; set; }

        public virtual Account ProvisionforOutstandingAccount { get; set; }

        //public virtual School School { get; set; }

        public virtual Account TaxLedgerAccount { get; set; }
    }
}