using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
{
    [Table("PaymentModes", Schema = "account")]
    public partial class PaymentMode
    {
        public PaymentMode()
        {
        }

        [Key]
        public int PaymentModeID { get; set; }

        [StringLength(50)]
        public string PaymentModeName { get; set; }

        public long? AccountId { get; set; }

        public int? TenderTypeID { get; set; }

        public byte? EntitlementID { get; set; }

        public virtual Account Account { get; set; }
    }
}