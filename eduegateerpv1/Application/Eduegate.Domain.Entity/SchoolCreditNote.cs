namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.SchoolCreditNote")]
    public partial class SchoolCreditNote
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchoolCreditNote()
        {
            CreditNoteFeeTypeMaps = new HashSet<CreditNoteFeeTypeMap>();
        }

        [Key]
        public long SchoolCreditNoteIID { get; set; }

        public int? ClassID { get; set; }

        public long? StudentID { get; set; }

        public DateTime? CreditNoteDate { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public decimal? Amount { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? SectionID { get; set; }

        public bool? IsDebitNote { get; set; }

        [StringLength(100)]
        public string CreditNoteNumber { get; set; }

        public long? AccountTransactionHeadID { get; set; }

        public bool? IsCancelled { get; set; }

        public DateTime? CancelledDate { get; set; }

        [StringLength(250)]
        public string CancelReason { get; set; }

        public virtual AccountTransactionHead AccountTransactionHead { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }

        public virtual School School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Student Student { get; set; }
    }
}
