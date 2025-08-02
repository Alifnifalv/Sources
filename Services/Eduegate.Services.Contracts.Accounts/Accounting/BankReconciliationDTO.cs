using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
    public class BankReconciliationDTO : BaseMasterDTO
    {
        public BankReconciliationDTO()
        {
            BankReconciliationTransactionDtos = new List<BankReconciliationTransactionDTO> ();           
        }

        [DataMember]
        public string IBAN { get; set; }

        [DataMember]
        public string AccountNo { get; set; }
        [DataMember]
        public long? BankAccountID { get; set; }

        [DataMember]
        public string BankName { get; set; }
        [DataMember]
        public DateTime? SDate { get; set; }

        [DataMember]
        public DateTime? EDate { get; set; }

        [DataMember]
        public long? BankReconciliationHeadIID { get; set; }

        [DataMember]
        public List<BankReconciliationTransactionDTO> BankReconciliationTransactionDtos { get; set; }

    }
    [DataContract]
    public class BankOpeningParametersDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    { 
        [DataMember]
        public long? BankAccountID { get; set; }

        [DataMember]
        public DateTime? SDate { get; set; }

        [DataMember]
        public DateTime? EDate { get; set; }

        [DataMember]
        public decimal? OpeningBalance { get; set; }

        [DataMember]
        public decimal? ClosingBalance { get; set; }

        [DataMember]
        public long? FeedLogID { get; set; }

        [DataMember]
        public long? BankReconciliationHeadIID { get; set; }

        [DataMember]
        public decimal? LedgerClosingBalance { get; set; }

        [DataMember]
        public decimal? BankClosingBalInput { get; set; }
    }
}
