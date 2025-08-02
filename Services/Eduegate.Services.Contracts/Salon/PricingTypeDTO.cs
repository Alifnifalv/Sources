using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Salon
{
    [DataContract]
    public class PricingTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int PricingTypeID { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
