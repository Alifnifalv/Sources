using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.ShoppingCart
{
     [DataContract]
   public class CartPaymentDetailsDTO
    {
        [DataMember]
        public int PaymentGateWayID { get; set; }
          [DataMember]
        public long CartIID { get; set; }
          [DataMember]
        public int CartStatusID { get; set; }
          [DataMember]
        public long TrackKey { get; set; }
           [DataMember]
          public string PaymentGateWay { get; set; }
    }
}
