using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BrandImageMap", Schema = "catalog")]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("BrandID")]
        [InverseProperty("BrandImageMaps")]
        public virtual Brand Brand { get; set; }
    }
}
