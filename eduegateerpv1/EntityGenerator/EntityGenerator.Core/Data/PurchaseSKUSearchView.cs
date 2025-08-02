using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class PurchaseSKUSearchView
    {
        public long HeadIID { get; set; }
        public int? DocumentTypeID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        public long ProductSKUMapIID { get; set; }
        [StringLength(1000)]
        public string SKU { get; set; }
        [StringLength(50)]
        public string PartNo { get; set; }
        [StringLength(50)]
        public string Barcode { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? UnitPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OrderDate { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [StringLength(50)]
        public string Area { get; set; }
        public int? CompanyID { get; set; }
    }
}
