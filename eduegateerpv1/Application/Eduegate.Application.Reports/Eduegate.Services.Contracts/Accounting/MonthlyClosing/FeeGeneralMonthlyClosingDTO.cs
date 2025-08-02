using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounting.MonthlyClosing
{
    [DataContract]
    public class FeeGeneralMonthlyClosingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FeeGeneralMonthlyClosingDTO()
        {

        }
        [DataMember]
        public int? FeeTypeID { get; set; } //Level 1
        [DataMember]
        public int? FeeCycleID { get; set; } //Level 2
        [DataMember]
        public int? FeeMasterID { get; set; } //Level 3	
        [DataMember]
        public decimal? OpeningDebit { get; set; }//Level 3	
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
        public string FeeTypeName { get; set; }
        [DataMember]
        public string FeeCycleName { get; set; }
        [DataMember]
        public string FeeMasterName { get; set; }
        [DataMember]
        public decimal? FeeAmount { get; set; }
        [DataMember]
        public decimal? AccountAmount { get; set; }
    }
}
