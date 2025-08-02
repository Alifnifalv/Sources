using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductFeeMaps", Schema = "catalog")]
    public partial class ProductFeeMap
    {
        [Key]
        public long ProductFeeMapIID { get; set; }
        public int? FeeMasterID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public byte[] TimeStamps { get; set; }
    }
}
