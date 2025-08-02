namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.BrandImageMap")]
    public partial class BrandImageMap
    {
        [Key]
        public long BrandImageMapIID { get; set; }

        public long? BrandID { get; set; }

        public byte? ImageTypeID { get; set; }

        [StringLength(200)]
        public string ImageFile { get; set; }

        [StringLength(1000)]
        public string ImageTitle { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
