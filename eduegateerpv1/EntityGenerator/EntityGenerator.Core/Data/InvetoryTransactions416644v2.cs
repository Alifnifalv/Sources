using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("InvetoryTransactions416644v2", Schema = "inventory")]
    public partial class InvetoryTransactions416644v2
    {
        public long InventoryTransactionIID { get; set; }
        public int? SerialNo { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(30)]
        public string TransactionNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        public long? ProductSKUMapID { get; set; }
        public long? BatchID { get; set; }
        public long? AccountID { get; set; }
        public long? UnitID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Cost { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Rate { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        public int? CurrencyID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExchangeRate { get; set; }
        public long? LinkDocumentID { get; set; }
        public long? BranchID { get; set; }
        public int? CompanyID { get; set; }
        public long? HeadID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LandingCost { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LastCostPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Fraction { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? OriginalQty { get; set; }
    }
}
