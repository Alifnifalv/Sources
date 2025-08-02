using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Budgeting
{
    [DataContract]
    public class BudgetEntryAllocationDTO 
    {
        [DataMember]
        public long BudgetEntryAllocationIID { get; set; }

        [DataMember]
        public long? BudgetEntryID { get; set; }

        [DataMember]
        public DateTime? SuggestedStartDate { get; set; }

        [DataMember]
        public DateTime? SuggestedEndDate { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public decimal? SuggestedValue { get; set; }

        [DataMember]
        public decimal? Percentage { get; set; }

        [DataMember]
        public decimal? EstimateValue { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public string Remarks { get; set; }

    }
}