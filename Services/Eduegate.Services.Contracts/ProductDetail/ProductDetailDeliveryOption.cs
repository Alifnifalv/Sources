using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.ProductDetail
{
    [DataContract]
    public class ProductDetailDeliveryOption : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductID { get; set; }
        
        [DataMember]
        public string DeliveryOption { get; set; }
        [DataMember]
        public long SkuID { get; set; }

        [DataMember]
        public long DeliveryTypeID { get; set; }

        [DataMember]
        public long DeliveryDays { get; set; }

        [DataMember]
        public string DeliveryOptionDisplayText { get; set; }

        [DataMember]
        public long DisplayRange { get; set; }
    }
}
