using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
    public class BankReconciliationTransactionDTO : BaseMasterDTO
    {

        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public long? AccountID { get; set; }

        [DataMember]
        public int? BankAccountID { get; set; }

        [DataMember]
        public string AccountNumber { get; set; }

        [DataMember]
        public string Narration { get; set; }

        [DataMember]
        public Decimal? Debit { get; set; }

        [DataMember]
        public Decimal? Credit { get; set; }

        [DataMember]
        public Decimal? Amount { get; set; }

        [DataMember]
        public DateTime? TransDate { get; set; }
        [DataMember]
        public string ChequeNo { get; set; }
        [DataMember]
        public string PartyName { get; set; }
        [DataMember]
        public string Reference { get; set; }
        [DataMember]
        public DateTime? ChequeDate { get; set; }
        [DataMember]
        public long? TranHeadID { get; set; }
        [DataMember]
        public long? TranTailID { get; set; }
        [DataMember]
        public long? SlNo { get; set; }
        [DataMember]
        public Decimal? OpeningBalance { get; set; }
        [DataMember]
        public Decimal? ClosingBalance { get; set; }

    }
}
