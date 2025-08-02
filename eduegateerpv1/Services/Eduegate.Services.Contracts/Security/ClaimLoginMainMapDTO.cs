using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Security
{
    [DataContract]
    public class ClaimLoginMainMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ClaimLoginMainMapIID { get; set; }

        [DataMember]
        public int? CompanyID { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public long? ClaimID { get; set; }

        [DataMember]
        public long AssociateTeacherID { get; set; }

        [DataMember]
        public List<ClaimLoginMapDTO> LoginMaps { get; set; }
    }
}