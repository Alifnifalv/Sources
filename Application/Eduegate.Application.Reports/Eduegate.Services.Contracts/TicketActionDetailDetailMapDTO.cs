using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    public class TicketActionDetailDetailMapDTO
    {
        public long TicketActionDetailDetailMapIID { get; set; }
        public long TicketActionDetailMapID { get; set; }
        public Nullable<int> Notify { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public string Timestamps { get; set; }
    }
}
