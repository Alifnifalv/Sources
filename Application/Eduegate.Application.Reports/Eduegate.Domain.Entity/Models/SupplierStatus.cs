using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierStatus
    {
        public SupplierStatus()
        {
            this.Suppliers = new List<Supplier>();
        }

        public byte SupplierStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
