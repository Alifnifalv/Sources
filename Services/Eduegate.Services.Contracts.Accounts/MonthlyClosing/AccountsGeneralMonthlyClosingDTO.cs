using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.MonthlyClosing
{
    [DataContract]
    public class AccountsGeneralMonthlyClosingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AccountsGeneralMonthlyClosingDTO()
        {

        }

        [DataMember]
        public long? MainGroupID { get; set; } //Level 1

        [DataMember]
        public long? SubGroupID { get; set; } //Level 2

        [DataMember]
        public long? GroupID { get; set; } //Level 3

        [DataMember]
        public string MainGroupName { get; set; }
        [DataMember]
        public string SubGroupName { get; set; }
        [DataMember]
        public string GroupName { get; set; }

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
