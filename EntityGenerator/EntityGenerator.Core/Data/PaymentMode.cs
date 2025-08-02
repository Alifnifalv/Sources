using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentModes", Schema = "account")]
    public partial class PaymentMode
    {
        public PaymentMode()
        {
            FeeCollectionPaymentModeMaps = new HashSet<FeeCollectionPaymentModeMap>();
            FinalSettlementPaymentModeMaps = new HashSet<FinalSettlementPaymentModeMap>();
            RefundPaymentModeMaps = new HashSet<RefundPaymentModeMap>();
        }

        [Key]
        public int PaymentModeID { get; set; }
        [StringLength(50)]
        public string PaymentModeName { get; set; }
        public long? AccountId { get; set; }
        public int? TenderTypeID { get; set; }
        public byte? EntitlementID { get; set; }

        [ForeignKey("AccountId")]
        [InverseProperty("PaymentModes")]
        public virtual Account Account { get; set; }
        [ForeignKey("TenderTypeID")]
        [InverseProperty("PaymentModes")]
        public virtual TenderType TenderType { get; set; }
        [InverseProperty("PaymentMode")]
        public virtual ICollection<FeeCollectionPaymentModeMap> FeeCollectionPaymentModeMaps { get; set; }
        [InverseProperty("PaymentMode")]
        public virtual ICollection<FinalSettlementPaymentModeMap> FinalSettlementPaymentModeMaps { get; set; }
        [InverseProperty("PaymentMode")]
        public virtual ICollection<RefundPaymentModeMap> RefundPaymentModeMaps { get; set; }
    }
}
