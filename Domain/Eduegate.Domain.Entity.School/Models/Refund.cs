namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Refund", Schema = "schools")]
    public partial class Refund
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Refund()
        {
            RefundFeeTypeMaps = new HashSet<RefundFeeTypeMap>();
            RefundPaymentModeMaps = new HashSet<RefundPaymentModeMap>();
        }

        [Key]
        public long RefundIID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public long? StudentID { get; set; }

        public DateTime? RefundDate { get; set; }

        public decimal? Amount { get; set; }

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

        public bool IsAccountPosted { get; set; }

        public int? AcadamicYearID { get; set; }

        public byte? SchoolID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual Schools School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Student Student { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefundFeeTypeMap> RefundFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefundPaymentModeMap> RefundPaymentModeMaps { get; set; }
    }
}
