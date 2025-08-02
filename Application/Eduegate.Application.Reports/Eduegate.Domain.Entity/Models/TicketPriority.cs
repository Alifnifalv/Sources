using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TicketPriority
    {
        public byte TicketPriorityID { get; set; }
        public string PriorityName { get; set; }
    }
}
