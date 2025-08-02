using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Supports
{
    public class TicketProductDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long TicketProductMapIID { get; set; }

        [DataMember]
        public long ProductID { get; set; }

        [DataMember]
        public long SKUID { get; set; }

        [DataMember]
        public string SKUName { get; set; }

        [DataMember]
        public Nullable<decimal> Quantity { get; set; }

        [DataMember]
        public long ReasonID { get; set; }

        [DataMember]
        public string Narration { get; set; }

        [DataMember]
        public long TicketID { get; set; }

        [DataMember]
        public Nullable<decimal> UnitPrice { get; set; }

        [DataMember]
        public string ProductImageUrl { get; set; }
    }
}
