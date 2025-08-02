using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("TicketReasons", Schema = "cs")]
    public partial class TicketReason
    {
        [Key]
        public short TicketReasonID { get; set; }
        [StringLength(50)]
        public string TicketReasonName { get; set; }
    }
}