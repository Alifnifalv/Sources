using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerStatus
    {
        public CustomerStatus()
        {
            this.Customers = new List<Customer>();
        }

        public byte CustomerStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
