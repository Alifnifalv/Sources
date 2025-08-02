using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
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

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}