using Eduegate.Web.Library.ViewModels;
using System;

namespace Eduegate.Web.Library.Budgeting.Budget
{
    public class AllocationAmountViewModel : BaseMasterViewModel
    {
        public AllocationAmountViewModel()
        {
        }

        public long BudgetEntryAllocationIID { get; set; }

        public long? BudgetEntryID { get; set; }

        public string SuggestedStartDateString { get; set; }
        public DateTime? SuggestedStartDate { get; set; }

        public string SuggestedEndDateString { get; set; }
        public DateTime? SuggestedEndDate { get; set; }

        public string StartDateString { get; set; }
        public DateTime? StartDate { get; set; }

        public string EndDateString { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal? SuggestedValue { get; set; } = 0;

        public decimal? Percentage { get; set; } = 0;

        public decimal? EstimateValue { get; set; } = 0;

        public decimal? Amount { get; set; }

        public int? MonthID { get; set; }

        public string MonthName { get; set; }

        public int? Year { get; set; }

        public string Remarks { get; set; }

    }
}