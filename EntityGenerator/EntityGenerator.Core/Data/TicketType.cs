using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TicketTypes", Schema = "cs")]
    public partial class TicketType
    {
        public TicketType()
        {
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public int TicketTypeID { get; set; }
        [StringLength(50)]
        public string TicketTypeName { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("TicketType")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
