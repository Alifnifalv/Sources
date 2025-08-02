using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    public class AccountTransactionReceivablesMapDTO : BaseMasterDTO
    {
        [DataMember]
        public long AccountTransactionReceivablesMapIID { get; set; }
        [DataMember]
        public long? ReceivableID { get; set; }
        [DataMember]
        public long? HeadID { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
    }
}
