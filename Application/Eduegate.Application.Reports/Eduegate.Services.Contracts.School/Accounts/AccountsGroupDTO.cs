using Eduegate.Framework.Contracts.Common;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Accounts
{
    [DataContract]
    public class AccountsGroupDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AccountsGroupDTO()
        {
            ParentGroup = new KeyValueDTO();
        }

        [DataMember]
        public int GroupID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string GroupCode { get; set; }

        [DataMember]
        [StringLength(100)]
        public string GroupName { get; set; }

        [DataMember]
        public int? Parent_ID { get; set; }

        [DataMember]
        public KeyValueDTO ParentGroup { get; set; }

        [DataMember]
        public int? Affect_ID { get; set; }
    }
}
