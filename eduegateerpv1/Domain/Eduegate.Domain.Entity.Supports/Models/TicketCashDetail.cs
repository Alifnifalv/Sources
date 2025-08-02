using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("TicketCashDetails", Schema = "cs")]
    public partial class TicketCashDetail
    {
        [Key]
        [Column("CashDetailID")]
        public long CashDetailId { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal RefundAmount { get; set; }
        [Column("ReferenceDetailID")]
        public long ReferenceDetailId { get; set; }
    }
}