using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounts.MonthlyClosing
{
    [DataContract]
    public class InvoiceGeneralMonthlyClosingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public InvoiceGeneralMonthlyClosingDTO()
        {

        }
        [DataMember]
        public int? TransactionTypeID { get; set; } //Level 1
        [DataMember]
        public int? DocumentTypeID { get; set; } //Level 2

        [DataMember]
        public string TransactionTypeName { get; set; }
        [DataMember]
        public string DocumentTypeName { get; set; }

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
       
      
    }
}
