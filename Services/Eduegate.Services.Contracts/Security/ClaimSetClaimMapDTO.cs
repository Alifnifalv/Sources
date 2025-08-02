using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Security
{
    [DataContract]
    public class ClaimSetClaimMapDTO : ClaimDTO 
    {
        [DataMember]
        public long ClaimSetClaimMapIID { get; set; }
    }
}
