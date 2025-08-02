using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Admin
{
    [DataContract]
    public class ClaimDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ClaimIID { get; set; }

        [DataMember]
        public string ClaimName { get; set; }

        [DataMember]
        public ClaimType ClaimType { get; set; }
    }
}
