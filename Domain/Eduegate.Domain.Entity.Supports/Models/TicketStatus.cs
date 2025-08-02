using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("TicketStatuses", Schema = "cs")]
    public partial class TicketStatus
    {
        public TicketStatus()
        {
            DocumentReferenceTicketStatusMaps = new HashSet<DocumentReferenceTicketStatusMap>();
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public byte TicketStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        public virtual ICollection<DocumentReferenceTicketStatusMap> DocumentReferenceTicketStatusMaps { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}