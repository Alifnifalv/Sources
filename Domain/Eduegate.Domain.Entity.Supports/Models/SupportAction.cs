using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("SupportActions", Schema = "cs")]
    public partial class SupportAction
    {
        public SupportAction()
        {
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public byte SupportActionID { get; set; }

        [StringLength(50)]
        public string ActionName { get; set; }

        public byte? TicketReferenceTypeID { get; set; }

        public int? SortOrder { get; set; }

        public virtual TicketReferenceType TicketReferenceType { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}