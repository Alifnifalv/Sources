using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.ProductDetail
{
    [DataContract]
    public class ProductSKUVariantDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductSKUMapIID { get; set; }
        [DataMember]
        public string PropertyTypeName { get; set; }
        [DataMember]
        public string PropertyName { get; set; }
        [DataMember]
        public long ProductIID { get; set; }

        [DataMember]
        public string ProductCode { get; set; }
    }
}
