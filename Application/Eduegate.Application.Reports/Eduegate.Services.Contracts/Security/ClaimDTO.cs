using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Security
{
    [DataContract]
    public class ClaimDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ClaimIID { get; set; }

        [DataMember]
        public string ClaimName { get; set; }

        [DataMember]
        public string ResourceName { get; set; }

        [DataMember]
        public ClaimType ClaimTypeID { get; set; }

        [DataMember]
        public string Rights { get; set; }
        [DataMember]
        public int? CompanyID { get; set; }
    }
}
