using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TicketReasons", Schema = "cs")]
    public partial class TicketReason
    {
        public TicketReason()
        {
            this.TicketProductMaps = new List<TicketProductMap>();
        }

        [Key]
        public Int16 TicketReasonID { get; set; }
        public string TicketReasonName { get; set; }
        public virtual ICollection<TicketProductMap> TicketProductMaps { get; set; }
    }
}
