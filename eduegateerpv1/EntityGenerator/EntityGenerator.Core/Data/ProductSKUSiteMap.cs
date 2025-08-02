using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductSKUSiteMap", Schema = "catalog")]
    public partial class ProductSKUSiteMap
    {
        [Key]
        public long ProductSKUSiteMapIID { get; set; }
        public long? ProductSKUMapID { get; set; }
        public int? SiteID { get; set; }
        public bool? IsActive { get; set; }
        public byte? CultureID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductSKUSiteMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
