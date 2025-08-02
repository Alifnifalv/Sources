using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Budgeting
{
    [DataContract]
    public class BudgetEntryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public BudgetEntryDTO()
        {           
            BudgetEntryAllocationDtos = new List<BudgetEntryAllocationDTO>();
            BudgetEntryAccountMapDtos= new List<BudgetEntryAccountMapDTO>();
            BudgetEntryCostCenterDtos = new List<BudgetEntryCostCenterMapDTO>();
        }

        [DataMember]
        public long BudgetEntryIID { get; set; }

        [DataMember]
        public KeyValueDTO AccountGroup { get; set; }

        [DataMember]
        public int? BudgetID { get; set; }

        [DataMember]
        public byte? BudgetTypeID { get; set; }

        [DataMember]
        public byte? BudgetSuggestionID { get; set; }

        [DataMember]
        public KeyValueDTO Budget { get; set; }

        [DataMember]
        public KeyValueDTO BudgetSuggestion { get; set; }

        [DataMember]
        public KeyValueDTO BudgetType { get; set; }

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
        public decimal? Amount { get; set; }

        [DataMember]
        public decimal? Percentage { get; set; }

        [DataMember]
        public decimal? EstimateValue { get; set; }

        [DataMember]
        public List<BudgetEntryAccountMapDTO> BudgetEntryAccountMapDtos { get; set; }

        [DataMember]
        public List<BudgetEntryAllocationDTO> BudgetEntryAllocationDtos { get; set; }

        [DataMember]
        public List<BudgetEntryCostCenterMapDTO> BudgetEntryCostCenterDtos { get; set; }

    }

}