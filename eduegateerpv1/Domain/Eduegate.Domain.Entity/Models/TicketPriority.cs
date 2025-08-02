using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TicketPriorities", Schema = "cs")]
    public partial class TicketPriority
    {
        [Key]
        public byte TicketPriorityID { get; set; }
        public string PriorityName { get; set; }
    }
}
