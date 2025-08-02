using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Services.Contracts.Distributions
{
    [DataContract]
    public class SKUBranchDeliveryTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }

        [DataMember]
        public Nullable<long> BranchID { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public List<ProductDeliveryTypeDTO> DeliveryDetails { get; set; }

    }
}
