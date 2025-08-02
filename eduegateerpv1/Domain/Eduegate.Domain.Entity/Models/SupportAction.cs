using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("SupportActions", Schema = "cs")]
    public partial class SupportAction
    {
        public SupportAction()
        {
            this.Tickets = new List<Ticket>();
        }

        [Key]
        public byte SupportActionID { get; set; }
        public string ActionName { get; set; }
        public int? ActionTypeID { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
