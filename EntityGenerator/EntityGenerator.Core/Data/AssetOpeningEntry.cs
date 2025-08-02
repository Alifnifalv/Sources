using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssetOpeningEntries", Schema = "asset")]
    public partial class AssetOpeningEntry
    {
        [Key]
        public long OpeningEntryIID { get; set; }
        public long? AssetTransactionHeadID { get; set; }
        [StringLength(100)]
        public string SlNo { get; set; }
        [StringLength(100)]
        public string Category { get; set; }
        public string AssetDescription { get; set; }
        [StringLength(100)]
        public string Reference { get; set; }
        [StringLength(100)]
        public string SupplierCode { get; set; }
        [StringLength(250)]
        public string Supplier { get; set; }
        public string BillNumber { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfPurchase { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfDepreciation { get; set; }
        [StringLength(100)]
        public string AssetLocation { get; set; }
        [StringLength(100)]
        public string Department { get; set; }
        [StringLength(100)]
        public string AssetUser { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CostAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LastDepAmount { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
