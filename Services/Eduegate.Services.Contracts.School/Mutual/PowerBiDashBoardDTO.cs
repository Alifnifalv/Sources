using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Mutual
{
    [DataContract]
    public class PowerBiDashBoardDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long Dsh_ID { get; set; }

        [DataMember]
        public long? MenuLinkID { get; set;}

        [DataMember]
        public long? PageID { get; set;}

        [DataMember]    
        public string ReportID { get; set;}

        [DataMember]
        public string WorkspaceID { get; set;}

        [DataMember]
        public string TenantID { get; set;}

        [DataMember]
        public string ClientID { get; set;}

        [DataMember]
        public string ClientSecret { get; set;}

        [DataMember]
        public string ClintToken { get; set;}

        [DataMember]
        public string Embededlink { get;}

    }
}


