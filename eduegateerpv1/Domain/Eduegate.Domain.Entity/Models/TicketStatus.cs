using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TicketStatuses", Schema = "cs")]
    public partial class TicketStatus
    {
        public TicketStatus()
        {
            this.Tickets = new List<Ticket>();
        }

        [Key]
        public byte TicketStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
