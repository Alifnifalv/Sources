using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductStudentMaps", Schema = "catalog")]
    [Index("AcademicYearID", "ProductID", "FeeMasterID", "ProductSKUMapID", "StudentID", Name = "IX_ProductStudentMaps_Unique", IsUnique = true)]
    public partial class ProductStudentMap
    {
        [Key]
        public long ProductStudentMapIID { get; set; }
        public long? StudentID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        public int? FeeMasterID { get; set; }
        [Column(TypeName = "money")]
        public decimal? FeeAmount { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public byte[] TimeStamps { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ProductStudentMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("FeeMasterID")]
        [InverseProperty("ProductStudentMaps")]
        public virtual FeeMaster FeeMaster { get; set; }
        [ForeignKey("ProductID")]
        [InverseProperty("ProductStudentMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductStudentMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("ProductStudentMaps")]
        public virtual Student Student { get; set; }
    }
}
