using Eduegate.Framework.Contracts.Common;
using Eduegate.Infrastructure.Enums;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
    public class AccountDTO : BaseMasterDTO
    {
        [DataMember]
        public long? AccountID { get; set; }
        [DataMember]
        public string Alias { get; set; }
        [DataMember]
        public long? ParentAccountID { get; set; }
        [DataMember]
        public string AccountCode { get; set; }
        [DataMember]
        public long? AccountGroupID { get; set; }

        [DataMember]
        public string AccountName { get; set; }
        [DataMember]
        public AccountDTO ParentAccount { get; set; }
        [DataMember]
        public AccountGroupDTO AccountGroup { get; set; }

        [DataMember]
        public AccountBehavior? AccountBehavior { get; set; }
    }
}
