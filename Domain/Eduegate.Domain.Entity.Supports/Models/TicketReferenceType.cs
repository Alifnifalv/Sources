using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("TicketReferenceTypes", Schema = "cs")]
    public partial class TicketReferenceType
    {
        public TicketReferenceType()
        {
            SupportActions = new HashSet<SupportAction>();
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public byte ReferenceTypeID { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string ReferenceTypeName { get; set; }

        public virtual ICollection<SupportAction> SupportActions { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}