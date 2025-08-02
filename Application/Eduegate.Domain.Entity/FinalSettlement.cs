namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.FinalSettlement")]
    public partial class FinalSettlement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FinalSettlement()
        {
            FinalSettlementFeeTypeMaps = new HashSet<FinalSettlementFeeTypeMap>();
            FinalSettlementPaymentModeMaps = new HashSet<FinalSettlementPaymentModeMap>();
        }

        [Key]
        public long FinalSettlementIID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public long? StudentID { get; set; }

        public DateTime? FinalSettlementDate { get; set; }

        public decimal? Amount { get; set; }

        public decimal? PaidAmount { get; set; }

        public bool? IsPaid { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(50)]
        public string FeeReceiptNo { get; set; }

        public bool IsAccountPosted { get; set; }

        public int? AcadamicYearID { get; set; }

        public byte? SchoolID { get; set; }

        public bool? IsCancelled { get; set; }

        public DateTime? CancelledDate { get; set; }

        [StringLength(250)]
        public string CancelReason { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual School School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Student Student { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinalSettlementFeeTypeMap> FinalSettlementFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinalSettlementPaymentModeMap> FinalSettlementPaymentModeMaps { get; set; }
    }
}
