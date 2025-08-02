using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
    public class TransactionHeadAccountMapDTO : BaseMasterDTO
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
