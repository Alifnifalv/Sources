using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class LoginRoleMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long LoginRoleMapIID { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public int? RoleID { get; set; }
    }
}
