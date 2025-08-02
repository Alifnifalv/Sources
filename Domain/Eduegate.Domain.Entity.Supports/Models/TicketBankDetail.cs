using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("TicketBankDetails", Schema = "cs")]
    public partial class TicketBankDetail
    {
        [Key]
        [Column("BankIID")]
        public long BankIid { get; set; }
        [Required]
        [StringLength(100)]
        public string BankName { get; set; }
        [StringLength(100)]
        public string AccountNumber { get; set; }
        [Column("ReferenceDetailID")]
        public long ReferenceDetailId { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal RefundAmount { get; set; }
    }
}