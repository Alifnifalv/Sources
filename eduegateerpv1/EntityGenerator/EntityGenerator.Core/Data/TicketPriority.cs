using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
