using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductClassMaps", Schema = "catalog")]
    public partial class ProductClassMap
    {
        [Key]
        public long ProductClassMapIID { get; set; }
        public int? ClassID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        public int? FeeMasterID { get; set; }
        public int? ProductClassTypeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public byte[] TimeStamps { get; set; }
        public bool? IsActive { get; set; }
        public int? AcademicYearID { get; set; }
        public int? SubjectID { get; set; }
        public int? StreamID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ProductClassMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("ProductClassMaps")]
        public virtual Class Class { get; set; }
    }
}
