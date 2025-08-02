namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FeeCollectionPaymentModeMaps", Schema = "schools")]
    public partial class FeeCollectionPaymentModeMap
    {
        [Key]
        public long FeeCollectionPaymentModeMapIID { get; set; }

        public long? FeeCollectionID { get; set; }

        public int? PaymentModeID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? TypeDate { get; set; }

        [StringLength(25)]
        public string ReferenceNo { get; set; }

        public decimal? RefundAmount { get; set; }

        public decimal? Balance { get; set; }

        public long? AccountTransactionDetailID { get; set; }

        public long? BankID { get; set; }

        public virtual Bank Bank { get; set; }

        public virtual AccountTransactionDetail AccountTransactionDetail { get; set; }

        public virtual PaymentMode PaymentMode { get; set; }

        public virtual FeeCollection FeeCollection { get; set; }
    }
}
