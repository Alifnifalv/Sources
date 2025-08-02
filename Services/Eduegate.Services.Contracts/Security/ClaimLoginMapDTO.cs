using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Security
{
    [DataContract]
    public class ClaimLoginMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ClaimLoginMapIID { get; set; }

        [DataMember]
        public int? CompanyID { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public long? ClaimID { get; set; }
    }
}