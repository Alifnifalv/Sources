using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Distributions
{
    [DataContract]
    public class ServiceProviderDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int ServiceProviderID { get; set; }

        [DataMember]
        public string ProviderCode { get; set; }

        [DataMember]
        public string ProviderName { get; set; }

        [DataMember]
        public Nullable<int> CountryID { get; set; }

        [DataMember]
        public Nullable<bool> IsActive { get; set; }

        [DataMember]
        public string ServiceProviderLink { get; set; }
    }
}
