using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
    public class AssetTransactionSummaryDetailDTO
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