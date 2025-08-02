using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        public int? ActionTypeID { get; set; }

        [InverseProperty("Action")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
