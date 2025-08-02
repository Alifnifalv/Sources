using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class TicketActionDetailMapsDTO
    {
        [DataMember]
        public long TicketActionDetailIID { get; set; }
        [DataMember]
        public long TicketID { get; set; }
        [DataMember]
        public Nullable<int> RefundTypeID { get; set; }
        [DataMember]
        public Nullable<decimal> RefundAmount { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public string ReturnNumber { get; set; }
        [DataMember]
        public Nullable<int> GiveItemTo { get; set; }
        [DataMember]
        public Nullable<long> CreatedBy { get; set; }
        [DataMember]
        public Nullable<long> UpdatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [DataMember]
        public Nullable<int> IssueType { get; set; }
        [DataMember]
        public KeyValueDTO AssignedEmployee { get; set; }
        [DataMember]
        public string Timestamps { get; set; }
        [DataMember]
        public List<TicketActionDetailDetailMapDTO> TicketActionDetailDetailMaps { get; set; }
        [DataMember]
        public string SubActionName { get; set; }
    }
}
