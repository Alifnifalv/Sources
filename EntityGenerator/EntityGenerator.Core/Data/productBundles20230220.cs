using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("productBundles20230220", Schema = "catalog")]
    public partial class productBundles20230220
    {
        public long BundleIID { get; set; }
        public long? FromProductID { get; set; }
        public long? ToProductID { get; set; }
        public long? FromProductSKUMapID { get; set; }
        public long? ToProductSKUMapID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? SellingPrice { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? CostPrice { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
    }
}
