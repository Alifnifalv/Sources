using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("PaymentModes", Schema = "account")]
    public partial class PaymentMode
    {
        public PaymentMode()
        {
            this.AccountTransactionHeads = new List<AccountTransactionHead>();
        }

        [Key]
        public int PaymentModeID { get; set; }
        public string PaymentModeName { get; set; }
        public long? AccountId { get; set; }
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
    }
}
