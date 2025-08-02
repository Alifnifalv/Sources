using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RefundPaymentModeMaps", Schema = "schools")]
    public partial class RefundPaymentModeMap
    {
        [Key]
        public long RefundPaymentModeMapIID { get; set; }
        public long? RefundID { get; set; }
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

        [ForeignKey("PaymentModeID")]
        [InverseProperty("RefundPaymentModeMaps")]
        public virtual PaymentMode PaymentMode { get; set; }
        [ForeignKey("RefundID")]
        [InverseProperty("RefundPaymentModeMaps")]
        public virtual Refund Refund { get; set; }
    }
}
