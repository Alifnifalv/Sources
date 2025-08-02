using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Circulars", Schema = "schools")]
    [Index("CircularStatusID", "ExpiryDate", Name = "IDX_Circulars_CircularStatusIDExpiryDate_SchoolID")]
    public partial class Circular
    {
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
        [Column(TypeName = "datetime")]
        public DateTime? CircularDate { get; set; }
        [Column(TypeName = "datetime")]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? AttachmentReferenceID { get; set; }

        [ForeignKey("AcadamicYearID")]
        [InverseProperty("Circulars")]
        public virtual AcademicYear AcadamicYear { get; set; }
        [ForeignKey("CircularPriorityID")]
        [InverseProperty("Circulars")]
        public virtual CircularPriority CircularPriority { get; set; }
        [ForeignKey("CircularStatusID")]
        [InverseProperty("Circulars")]
        public virtual CircularStatus CircularStatus { get; set; }
        [ForeignKey("CircularTypeID")]
        [InverseProperty("Circulars")]
        public virtual CircularType CircularType { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Circulars")]
        public virtual School School { get; set; }
        [InverseProperty("Circular")]
        public virtual ICollection<CircularAttachmentMap> CircularAttachmentMaps { get; set; }
        [InverseProperty("Circular")]
        public virtual ICollection<CircularMap> CircularMaps { get; set; }
        [InverseProperty("Circular")]
        public virtual ICollection<CircularUserTypeMap> CircularUserTypeMaps { get; set; }
    }
}
