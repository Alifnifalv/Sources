using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("TicketPriorities", Schema = "cs")]
    public partial class TicketPriority
    {
        [Key]
        public byte TicketPriorityID { get; set; }
        [StringLength(50)]
        public string PriorityName { get; set; }
    }
}