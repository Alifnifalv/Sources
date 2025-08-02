using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Budgeting
{
    [DataContract]
    public class AccountSuggesionMapDTO
    {


        [DataMember]
        public long? BudgetEntryID { get; set; }

        [DataMember]
        public int? GroupID { get; set; }

        [DataMember]
        public long? AccountID { get; set; }

        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public KeyValueDTO Account { get; set; }

        [DataMember]
        public KeyValueDTO Group { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public int? MonthID { get; set; }

        [DataMember]
        public int? Year { get; set; }

        [DataMember]
        public string GroupDefaultSide { get; set; }

    }
}