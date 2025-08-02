using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BrandTagMaps", Schema = "catalog")]
    public partial class BrandTagMap
    {
        [Key]
        public long BrandTagMapIID { get; set; }
        public long? BrandTagID { get; set; }
        public long? BrandID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("BrandID")]
        [InverseProperty("BrandTagMaps")]
        public virtual Brand Brand { get; set; }
        [ForeignKey("BrandTagID")]
        [InverseProperty("BrandTagMaps")]
        public virtual BrandTag BrandTag { get; set; }
    }
}
