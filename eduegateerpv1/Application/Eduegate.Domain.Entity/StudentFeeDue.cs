namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentFeeDues")]
    public partial class StudentFeeDue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentFeeDue()
        {
            FeeDueCancellations = new HashSet<FeeDueCancellation>();
            FeeDueFeeTypeMaps = new HashSet<FeeDueFeeTypeMap>();
            FeeDueInventoryMaps = new HashSet<FeeDueInventoryMap>();
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
        }

        [Key]
        public long StudentFeeDueIID { get; set; }

        public int? ClassId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? StudentId { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public DateTime? DueDate { get; set; }

        [StringLength(50)]
        public string InvoiceNo { get; set; }

        public bool CollectionStatus { get; set; }

        public bool? IsAccountPost { get; set; }

        public DateTime? AccountPostingDate { get; set; }

        public int? AcadamicYearID { get; set; }

        public int? SectionID { get; set; }

        public byte? SchoolID { get; set; }

        public bool? IsCancelled { get; set; }

        public DateTime? CancelledDate { get; set; }

        [StringLength(250)]
        public string CancelReason { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueCancellation> FeeDueCancellations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueInventoryMap> FeeDueInventoryMaps { get; set; }

        public virtual School School { get; set; }

        public virtual Section Section { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }

        public virtual Student Student { get; set; }
    }
}
