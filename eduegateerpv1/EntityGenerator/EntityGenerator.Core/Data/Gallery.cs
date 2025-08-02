using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Galleries", Schema = "mutual")]
    public partial class Gallery
    {
        public Gallery()
        {
            GalleryAttachmentMaps = new HashSet<GalleryAttachmentMap>();
        }

        [Key]
        public long GalleryIID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? GalleryDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public bool? ISActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Galleries")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Galleries")]
        public virtual School School { get; set; }
        [InverseProperty("Gallery")]
        public virtual ICollection<GalleryAttachmentMap> GalleryAttachmentMaps { get; set; }
    }
}
