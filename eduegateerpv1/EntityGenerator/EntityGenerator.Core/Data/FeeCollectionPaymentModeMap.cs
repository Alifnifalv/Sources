using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeCollectionPaymentModeMaps", Schema = "schools")]
    [Index("AccountTransactionDetailID", Name = "IDX_FeeCollectionPaymentModeMaps_AccountTransactionDetailID_")]
    [Index("FeeCollectionID", Name = "IDX_FeeCollectionPaymentModeMaps_FeeCollectionID")]
    [Index("FeeCollectionID", Name = "idx_FeeCollectionPaymentModeMapsFeeCollectionID")]
    [Index("PaymentModeID", Name = "idx_FeeCollectionPaymentModeMapsPaymentModeID")]
    public partial class FeeCollectionPaymentModeMap
    {
        [Key]
        public long FeeCollectionPaymentModeMapIID { get; set; }
        public long? FeeCollectionID { get; set; }
        public int? PaymentModeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TypeDate { get; set; }
        [StringLength(25)]
        public string ReferenceNo { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? RefundAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Balance { get; set; }
        public long? AccountTransactionDetailID { get; set; }
        public long? BankID { get; set; }

        [ForeignKey("AccountTransactionDetailID")]
        [InverseProperty("FeeCollectionPaymentModeMaps")]
        public virtual AccountTransactionDetail AccountTransactionDetail { get; set; }
        [ForeignKey("BankID")]
        [InverseProperty("FeeCollectionPaymentModeMaps")]
        public virtual Bank Bank { get; set; }
        [ForeignKey("FeeCollectionID")]
        [InverseProperty("FeeCollectionPaymentModeMaps")]
        public virtual FeeCollection FeeCollection { get; set; }
        [ForeignKey("PaymentModeID")]
        [InverseProperty("FeeCollectionPaymentModeMaps")]
        public virtual PaymentMode PaymentMode { get; set; }
    }
}
