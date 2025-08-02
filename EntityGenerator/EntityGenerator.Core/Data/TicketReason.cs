using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TicketReasons", Schema = "cs")]
    public partial class TicketReason
    {
        [Key]
        public short TicketReasonID { get; set; }
        [StringLength(50)]
        public string TicketReasonName { get; set; }
    }
}
