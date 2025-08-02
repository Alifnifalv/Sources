using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
  public  class FeeDueDetail2MonthlyClosingViewModel : BaseMasterViewModel
    {
        public FeeDueDetail2MonthlyClosingViewModel()
        {
            FeeDueFeeMasterDetail = new List<FeeDueDetail3MonthlyClosingViewModel>();
        }
        public bool IsExpand { get; set; } = false;
        public int? FeeCycleID { get; set; } //Level 2
        public decimal? OpeningDebit { get; set; }//Level 3	

        public decimal? OpeningCredit { get; set; }


        public decimal? OpeningAmount { get; set; }

        public decimal? TransactionDebit { get; set; }

        public decimal? TransactionCredit { get; set; }

        public decimal? TransactionAmount { get; set; }

        public decimal? ClosingDebit { get; set; }

        public decimal? ClosingCredit { get; set; }

        public decimal? ClosingAmount { get; set; }      

        public string FeeCycleName { get; set; }
        public decimal? DifferenceAmount { get; set; }

        public decimal? DifferencePlusKPI { get; set; }

        public List<FeeDueDetail3MonthlyClosingViewModel> FeeDueFeeMasterDetail { get; set; }

    }
}
