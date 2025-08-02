using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
    public class BudgetingAccountsDTO 
    {
        public BudgetingAccountsDTO()
        {

        }
        [DataMember]
        public long? AccountID { get; set; }
        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public decimal? EstimateValue { get; set; } = 0;

        [DataMember]
        public decimal? PercentageValue { get; set; } = 0;

        [DataMember]
        public decimal? Amount1 { get; set; } = 0;

        [DataMember]
        public decimal? Amount2 { get; set; } = 0;

        [DataMember]
        public decimal? Amount3 { get; set; } = 0;

        [DataMember]
        public decimal? Amount4 { get; set; } = 0;

        [DataMember]
        public decimal? Amount5 { get; set; } = 0;
        [DataMember]
        public decimal? Amount6 { get; set; } = 0;
        [DataMember]
        public decimal? Amount7 { get; set; } = 0;
        [DataMember]
        public decimal? Amount8 { get; set; } = 0;
        [DataMember]
        public decimal? Amount9 { get; set; } = 0;
        [DataMember]
        public decimal? Amount10 { get; set; } = 0;
        [DataMember]
        public decimal? Amount11 { get; set; } = 0;
        [DataMember]
        public decimal? Amount12 { get; set; } = 0;
    }
}
