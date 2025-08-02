using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TicketProcessingStatuses", Schema = "cs")]
    public partial class TicketProcessingStatus
    {
        public TicketProcessingStatus()
        {
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public byte TicketProcessingStatusIID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("TicketProcessingStatus")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
