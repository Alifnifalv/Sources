using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("FeeCollections", Schema = "schools")]
    public partial class FeeCollection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeeCollection()
        {
            FeeCollectionFeeTypeMaps = new HashSet<FeeCollectionFeeTypeMap>();
            FeeCollectionPaymentModeMaps = new HashSet<FeeCollectionPaymentModeMap>();
        }

        [Key]
        public long FeeCollectionIID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public long? StudentID { get; set; }

        public int? FeeMasterID { get; set; }

        public DateTime? CollectionDate { get; set; }

        public decimal? Amount { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? FineAmount { get; set; }

        public decimal? PaidAmount { get; set; }

        public bool? IsPaid { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        [StringLength(50)]
        public string FeeReceiptNo { get; set; }

        public long? ClassFeeMasterId { get; set; }

        public bool IsAccountPosted { get; set; }

        public int? AcadamicYearID { get; set; }

        public long? AccountTransactionHeadID { get; set; }

        public byte? SchoolID { get; set; }

        public long? CashierID { get; set; }

        public string Remarks { get; set; }

        public bool? IsCancelled { get; set; }

        public DateTime? CancelledDate { get; set; }

        [StringLength(250)]
        public string CancelReason { get; set; }

        public int? FeeCollectionStatusID { get; set; }

        [StringLength(50)]
        public string GroupTransactionNumber { get; set; }

        public long? PaymentTrackID { get; set; }

        public virtual AccountTransactionHead AccountTransactionHead { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual ClassFeeMaster ClassFeeMaster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollectionPaymentModeMap> FeeCollectionPaymentModeMaps { get; set; }

        public virtual FeeCollectionStatus FeeCollectionStatus { get; set; }

        public virtual Schools School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Student Student { get; set; }

        public virtual FeeMaster FeeMaster { get; set; }
    }
}