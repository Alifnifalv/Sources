namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.PaymentEntries")]
    public partial class PaymentEntry
    {
        [Key]
        public long PayrollGenerationIID { get; set; }

        public int? CompanyID { get; set; }

        public int? BranchID { get; set; }

        public DateTime? PostingDate { get; set; }

        public int? DepartmentID { get; set; }

        public int? PayrollFrequencyID { get; set; }

        public int? DesignationID { get; set; }

        public bool? IsBasedOnTimeSheet { get; set; }

        public DateTime? StartPeriod { get; set; }

        public DateTime? EndPeriod { get; set; }

        public int? CostCenter { get; set; }

        public long? ProjectID { get; set; }

        public long? PaymentAccountID { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }
    }
}
