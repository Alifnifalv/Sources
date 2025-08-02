using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    public class AccountTransactionAmountDetail
    {
        [Key]
        public long AccountID { get; set; }
        public decimal TotalAmount { get; set; }
    }
}