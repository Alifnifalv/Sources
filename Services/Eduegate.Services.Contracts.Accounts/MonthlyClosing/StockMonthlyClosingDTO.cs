using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounts.MonthlyClosing
{
    [DataContract]
   public class StockMonthlyClosingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StockMonthlyClosingDTO()
        {

        }

        
        [DataMember]
        public int? TypeID { get; set; }
        [DataMember]
        public long? AccountID { get; set; }

        [DataMember]
        public string TypeName { get; set; }
        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public decimal? OpeningDebit { get; set; }
        [DataMember]
        public decimal? OpeningCredit { get; set; }

        [DataMember]
        public decimal? OpeningAmount { get; set; }
        [DataMember]
        public decimal? TransactionDebit { get; set; }
        [DataMember]
        public decimal? TransactionCredit { get; set; }
        [DataMember]
        public decimal? TransactionAmount { get; set; }
        [DataMember]
        public decimal? ClosingDebit { get; set; }
        [DataMember]
        public decimal? ClosingCredit { get; set; }
        [DataMember]
        public decimal? ClosingAmount { get; set; }
        [DataMember]
        public decimal? DifferenceAmount { get; set; }
        [DataMember]
        public decimal? DifferencePlusKPI { get; set; }
    }
}

