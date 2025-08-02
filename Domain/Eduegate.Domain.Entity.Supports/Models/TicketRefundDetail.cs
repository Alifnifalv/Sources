using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("TicketRefundDetails", Schema = "cs")]
    public partial class TicketRefundDetail
    {
        [Key]
        [Column("DetailIID")]
        public long DetailIid { get; set; }
        [Column("RefundTypeID")]
        public int RefundTypeId { get; set; }
        [Column("ReferenceTicketID")]
        public long ReferenceTicketId { get; set; }
        public string ReturnNumber { get; set; }
        [Column("StatusID")]
        public int StatusId { get; set; }
    }
}