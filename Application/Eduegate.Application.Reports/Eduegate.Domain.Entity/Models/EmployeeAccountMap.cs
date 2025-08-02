using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EmployeeAccountMap
    {
        public long EmployeeAccountMapIID { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public virtual Account Account { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
