using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
    public class BankReconciliationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
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
