using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Security
{
    [DataContract]
    public class ClaimSetDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ClaimSetIID { get; set; }
        [DataMember]
        public string ClaimSetName { get; set; }
        [DataMember]
        public int? CompanyID { get; set; }
        [DataMember]
        public List<ClaimSetClaimMapDTO> Claims { get; set; }
        [DataMember]
        public List<ClaimSetClaimSetMapDTO> ClaimSets { get; set; }
    }
}
