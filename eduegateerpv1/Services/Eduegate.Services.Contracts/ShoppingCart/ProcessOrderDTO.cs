using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.ShoppingCart
{
    [DataContract]
    public class ProcessOrderDTO
    {
        [DataMember]
        public long CartID { get; set; }
        [DataMember]
        public int CartStatus { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string PaymentMethod { get; set; }
        [DataMember]
        public long ShoppingCartIID { get; set; }
        [DataMember]
        public Int16 PaymentgateWayID { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}
