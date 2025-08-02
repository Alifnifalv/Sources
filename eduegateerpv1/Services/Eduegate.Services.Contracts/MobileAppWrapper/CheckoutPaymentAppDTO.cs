using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
     [DataContract]
    public class CheckoutPaymentAppDTO
    {
         [DataMember]
         public string CustomerID { get; set; }
         [DataMember]
         public string Amount { get; set; }
         [DataMember]
         public string InitiatedFromIP { get; set; }

         [DataMember]
         public string EmailId { get; set; }

         [DataMember]
         public int PaymentGateway { get; set; }
    }
}
