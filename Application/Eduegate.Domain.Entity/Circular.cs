namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.Circulars")]
    public partial class Circular
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Circular()
        {
            CircularAttachmentMaps = new HashSet<CircularAttachmentMap>();
            CircularMaps = new HashSet<CircularMap>();
            CircularUserTypeMaps = new HashSet<CircularUserTypeMap>();
        }

        [Key]
        public long CircularIID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcadamicYearID { get; set; }

        public byte? CircularTypeID { get; set; }

        public byte? CircularPriorityID { get; set; }

        public DateTime? CircularDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [StringLength(50)]
        public string CircularCode { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(500)]
        public string ShortTitle { get; set; }

        public string Message { get; set; }

        public byte? CircularStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? AttachmentReferenceID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CircularAttachmentMap> CircularAttachmentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CircularMap> CircularMaps { get; set; }

        public virtual CircularPriority CircularPriority { get; set; }

        public virtual School School { get; set; }

        public virtual CircularStatus CircularStatus { get; set; }

        public virtual CircularType CircularType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CircularUserTypeMap> CircularUserTypeMaps { get; set; }
    }
}
