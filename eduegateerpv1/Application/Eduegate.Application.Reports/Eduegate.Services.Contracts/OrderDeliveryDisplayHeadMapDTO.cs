using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
   [DataContract]
   public class OrderDeliveryDisplayHeadMapDTO
    {
       [DataMember]
        public long HeadID { get; set; }
       [DataMember]
        public byte CultureID { get; set; }
       [DataMember]
        public string DeliveryDisplayText { get; set; }
    }
}
