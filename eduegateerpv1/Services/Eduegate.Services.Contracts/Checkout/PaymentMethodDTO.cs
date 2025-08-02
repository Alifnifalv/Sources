using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Checkout
{
    [DataContract]
    public class PaymentMethodDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public short PaymentMethodID { get; set; }
        [DataMember]
        public string PaymentMethodName { get; set; }
        [DataMember]
        public string ImageName { get; set; }
        [DataMember]
        public bool? IsVirtual { get; set; }
        [DataMember]
        public bool ShowIfZero { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
