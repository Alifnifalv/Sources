using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductTagMaps", Schema = "catalog")]
    public partial class ProductTagMap
    {
        [Key]
        public long ProductTagMapIID { get; set; }
        public long? ProductTagID { get; set; }
        public long? ProductID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductTagMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductTagID")]
        [InverseProperty("ProductTagMaps")]
        public virtual ProductTag ProductTag { get; set; }
    }
}
