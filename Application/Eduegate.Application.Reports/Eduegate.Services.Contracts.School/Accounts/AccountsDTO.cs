using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Accounts
{
    [DataContract]
    public class AccountsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AccountsDTO()
        {
            AccountGroup = new KeyValueDTO();
            ParentAccount = new KeyValueDTO();
        }

        [DataMember]
        public long AccountID { get; set; }

        [DataMember]
        public string Alias { get; set; }

        [DataMember]
        public string AccountCode { get; set; }

        [DataMember]
        public long? ParentAccountID { get; set; }

        [DataMember]
        public int? GroupID { get; set; }

        [DataMember]
        public byte? AccountBehavoirID { get; set; }

        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public KeyValueDTO ParentAccount { get; set; }

        [DataMember]
        public KeyValueDTO AccountGroup { get; set; }

        [DataMember]
        public string AccountBehavior { get; set; }

        [DataMember]
        public bool? IsEnableSubLedger { get; set; }
    }
}
