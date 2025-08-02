namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.InvetoryTransactions20221227GRN")]
    public partial class InvetoryTransactions20221227GRN
    {
        [Key]
        public long InventoryTransactionIID { get; set; }

        public int? SerialNo { get; set; }

        public int? DocumentTypeID { get; set; }

        [StringLength(30)]
        public string TransactionNo { get; set; }

        public DateTime? TransactionDate { get; set; }

        public long? ProductSKUMapID { get; set; }

        public long? BatchID { get; set; }

        public long? AccountID { get; set; }

        public long? UnitID { get; set; }

        public decimal? Cost { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? Amount { get; set; }

        public decimal? Rate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public decimal? Discount { get; set; }

        public DateTime? DueDate { get; set; }

        public int? CurrencyID { get; set; }

        public decimal? ExchangeRate { get; set; }

        public long? LinkDocumentID { get; set; }

        public long? BranchID { get; set; }

        public int? CompanyID { get; set; }

        public long? HeadID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public decimal? LandingCost { get; set; }

        public decimal? LastCostPrice { get; set; }

        public decimal? Fraction { get; set; }

        public decimal? OriginalQty { get; set; }
    }
}
