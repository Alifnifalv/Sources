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
    public class TransactionHeadAccountMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long TransactionHeadAccountMapIID { get; set; }
        [DataMember]
        public Nullable<long> TransactionHeadID { get; set; }
        [DataMember]
        public Nullable<long> AccountTransactionID { get; set; }
        [DataMember]
        public AccountTransactionsDTO AccountTransaction { get; set; }       


    }
}
