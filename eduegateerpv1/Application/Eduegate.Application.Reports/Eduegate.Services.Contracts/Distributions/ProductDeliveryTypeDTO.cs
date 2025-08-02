using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Distributions
{
    [DataContract]
    public class ProductDeliveryTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {  
        [DataMember]
        public long ProductDeliveryTypeMapIID { get; set; }

        [DataMember]
        public Nullable<long> ProductID { get; set; }

        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }

        [DataMember]
        public Nullable<int> DeliveryTypeID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Nullable<decimal> DeliveryCharge { get; set; }

        [DataMember]
        public Nullable<decimal> DeliveryChargePercentage { get; set; }

        [DataMember]
        public Nullable<bool> IsDeliveryAvailable { get; set; }

        [DataMember]
        public Nullable<decimal> CartTotalFrom { get; set; }

        [DataMember]
        public Nullable<decimal> CartTotalTo { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }

        [DataMember]
        public Nullable<byte> DeliveryDays { get; set; }

        [DataMember]
        public Nullable<long> BranchID { get; set; }

        [DataMember]
        public string BranchName { get; set; } 

    }
}
