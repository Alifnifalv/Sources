using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ProductDeliverySettingSearchView
    {
        public long ProductIID { get; set; }
        public string ProductSKUMapIID { get; set; }
        [Required]
        public string PartNumber { get; set; }
        public string CategoryName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [Required]
        [StringLength(150)]
        public string ProductCode { get; set; }
        public long? BrandID { get; set; }
        [StringLength(1000)]
        public string ProductName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Weight { get; set; }
        [StringLength(50)]
        public string BrandName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string BrandCode { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
    }
}
