namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.AccountTransactions20221227pi")]
    public partial class AccountTransactions20221227pi
    {
        [Key]
        public long TransactionIID { get; set; }

        public int? DocumentTypeID { get; set; }

        [StringLength(50)]
        public string TransactionNumber { get; set; }

        public long? AccountID { get; set; }

        public decimal? Amount { get; set; }

        public decimal? InclusiveTaxAmount { get; set; }

        public decimal? ExclusiveTaxAmount { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public bool? DebitOrCredit { get; set; }

        public DateTime? TransactionDate { get; set; }

        public int? CostCenterID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }
    }
}
