using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductToProductMaps", Schema = "catalog")]
    public partial class ProductToProductMap
    {
        [Key]
        public long ProductToProductMapIID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductIDTo { get; set; }
        public byte? SalesRelationTypeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductToProductMapProducts")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductIDTo")]
        [InverseProperty("ProductToProductMapProductIDToNavigations")]
        public virtual Product ProductIDToNavigation { get; set; }
    }
}
