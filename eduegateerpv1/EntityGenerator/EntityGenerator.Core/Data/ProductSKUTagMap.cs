using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductSKUTagMaps", Schema = "catalog")]
    public partial class ProductSKUTagMap
    {
        [Key]
        public long ProductSKUTagMapIID { get; set; }
        public long? ProductSKUTagID { get; set; }
        public long? ProductSKuMapID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CompanyID { get; set; }

        [ForeignKey("ProductSKUTagID")]
        [InverseProperty("ProductSKUTagMaps")]
        public virtual ProductSKUTag ProductSKUTag { get; set; }
        [ForeignKey("ProductSKuMapID")]
        [InverseProperty("ProductSKUTagMaps")]
        public virtual ProductSKUMap ProductSKuMap { get; set; }
    }
}
