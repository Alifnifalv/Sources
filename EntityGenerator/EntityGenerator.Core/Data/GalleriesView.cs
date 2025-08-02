using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class GalleriesView
    {
        public long GalleryIID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? GalleryDate { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        public byte? SchoolID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
    }
}
