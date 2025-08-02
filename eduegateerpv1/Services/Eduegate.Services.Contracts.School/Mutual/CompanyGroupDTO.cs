using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Mutual
{
    [DataContract]
    public class CompanyGroupDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public int  CompanyGroupID { get; set; }
        [DataMember]
        public string  GroupName { get; set; }
    }
}


