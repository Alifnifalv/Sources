using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.HR.Payroll
{
    [Table("TicketEntitlementEntryStatuses", Schema = "payroll")]
    public partial class TicketEntitlementEntryStatus
    {
        public TicketEntitlementEntryStatus()
        {
            TicketEntitilementEntries = new HashSet<TicketEntitilementEntry>();
        }

        [Key]
        public short StatusID { get; set; }
        [Required]
        [StringLength(250)]
        public string StatusName { get; set; }

        [InverseProperty("TicketEntitlementEntryStatus")]
        public virtual ICollection<TicketEntitilementEntry> TicketEntitilementEntries { get; set; }
    }
}
