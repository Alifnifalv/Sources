using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TicketReason
    {
        public TicketReason()
        {
            this.TicketProductMaps = new List<TicketProductMap>();
        }

        public Int16 TicketReasonID { get; set; }
        public string TicketReasonName { get; set; }
        public virtual ICollection<TicketProductMap> TicketProductMaps { get; set; }
    }
}
