namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs.TicketPriorities")]
    public partial class TicketPriority
    {
        public byte TicketPriorityID { get; set; }

        [StringLength(50)]
        public string PriorityName { get; set; }
    }
}
