using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class KnowHowOption
    {
        public KnowHowOption()
        {
            this.Customers = new List<Customer>();
        }

        public long KnowHowOptionIID { get; set; }
        public string KnowHowOptionText { get; set; }
        public bool IsEditable { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
