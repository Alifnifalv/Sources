using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TransactionHeadEntitlementMap", Schema = "inventory")]
    public partial class TransactionHeadEntitlementMap
    {
        [Key]
        public long TransactionHeadEntitlementMapIID { get; set; }

        public long TransactionHeadID { get; set; }

        public byte EntitlementID { get; set; }

        public decimal? Amount { get; set; }

        public string ReferenceNo { get; set; }

        public long? PaymentTrackID { get; set; }

        public string PaymentTransactionNumber { get; set; }

        public virtual TransactionHead TransactionHead { get; set; }

        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
    }
}