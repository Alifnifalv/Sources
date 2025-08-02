using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("FinalSettlementPaymentModeMaps", Schema = "schools")]
    public partial class FinalSettlementPaymentModeMap
    {
        [Key]
        public long FinalSettlementPaymentModeMapIID { get; set; }

        public long? FinalSettlementID { get; set; }

        public int? PaymentModeID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        //public byte[] TimeStamps { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? TypeDate { get; set; }

        [StringLength(25)]
        public string ReferenceNo { get; set; }

        public decimal? RefundAmount { get; set; }

        public decimal? Balance { get; set; }

        public virtual PaymentMode PaymentMode { get; set; }

        public virtual FinalSettlement FinalSettlement { get; set; }
    }
}
