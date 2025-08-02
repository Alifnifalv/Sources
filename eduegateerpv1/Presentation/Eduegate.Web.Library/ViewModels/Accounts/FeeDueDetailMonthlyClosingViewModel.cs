using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class FeeDueDetailMonthlyClosingViewModel : BaseMasterViewModel
    {
        public FeeDueDetailMonthlyClosingViewModel()
        {
            FeeDueFeeCycleDetail = new List<FeeDueDetail2MonthlyClosingViewModel>();
        }

        public bool IsExpand { get; set; } = false;
        public int? FeeTypeID { get; set; } //Level 1

        public decimal? OpeningDebit { get; set; }

        public decimal? OpeningCredit { get; set; }

        public decimal? OpeningAmount { get; set; }

        public decimal? TransactionDebit { get; set; }

        public decimal? TransactionCredit { get; set; }

        public decimal? TransactionAmount { get; set; }

        public decimal? ClosingDebit { get; set; }

        public decimal? ClosingCredit { get; set; }

        public decimal? ClosingAmount { get; set; }

        public string FeeTypeName { get; set; }

        public decimal? DifferenceAmount { get; set; }

        public decimal? DifferencePlusKPI { get; set; }

        public List<FeeDueDetail2MonthlyClosingViewModel> FeeDueFeeCycleDetail { get; set; }
    }
}
