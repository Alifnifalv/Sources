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
    public class    ChartOfAccountLedgerDetailsDTO : BaseMasterDTO
    {
        [DataMember]
        public long ChartOfAccountMapIID { get; set; }

        [DataMember]
        public Nullable<long> AccountID { get; set; }

        [DataMember]
        public Nullable<int> GroupID { get; set; }        

        [DataMember]
        public string LedgerCode { get; set; }

        [DataMember]
        public string LedgerName { get; set; }

        [DataMember]
        public string LedgerGroup { get; set; }
        [DataMember]
        public string Company { get; set; }
        [DataMember]
        public decimal? OpeningBalance { get; set; }

        [DataMember]
        public decimal? ClosingBalance { get; set; }

        [DataMember]
        public decimal? Debit { get; set; }

        [DataMember]
        public decimal? Credit { get; set; }
    }
}
