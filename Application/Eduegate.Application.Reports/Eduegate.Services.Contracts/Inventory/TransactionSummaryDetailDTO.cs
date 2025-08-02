using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Inventory
{
    [DataContract]
    public class TransactionSummaryDetailDTO
    {
        [DataMember]
        public string TransactionTypeName { get; set; }
        [DataMember]
        public string TransactionNo { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public long? TransactionCount { get; set; }


        //For Dashboard chart
        [DataMember]
        public decimal? DailyAmount { get; set; } 

        [DataMember]
        public decimal? MonthlyAmount { get; set; }

        [DataMember]
        public decimal? YearlyAmount { get; set; }
    }
}
