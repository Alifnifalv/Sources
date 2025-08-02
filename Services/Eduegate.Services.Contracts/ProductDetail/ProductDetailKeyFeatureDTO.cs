using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.ProductDetail
{
    [DataContract]
    public class ProductDetailKeyFeatureDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long SKUID { get; set; }
        [DataMember]
        public string FeatureName { get; set; }
        [DataMember]
        public string FeatureValue { get; set; }

        [DataMember]
        public byte PropertyTypeID { get; set; }
        [DataMember]
        public long PropertyIID { get; set; }
    }
}
