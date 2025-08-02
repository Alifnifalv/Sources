using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums.Accounting;

namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
    public class AccountDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long? AccountID { get; set; }
        [DataMember]
        public string Alias { get; set; }
        [DataMember]

        public string AccountCode { get; set; }
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
