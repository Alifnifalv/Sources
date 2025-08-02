namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductStudentMaps")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamps { get; set; }

        public bool? IsActive { get; set; }

        public virtual Product Product { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual FeeMaster FeeMaster { get; set; }

        public virtual Student Student { get; set; }
    }
}
