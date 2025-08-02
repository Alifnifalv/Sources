using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
   public class FeeDueMainMonthlyClosingViewModel : BaseMasterViewModel
    {

        public FeeDueMainMonthlyClosingViewModel()
        {
            FeeDueDetailMonthlyClosingViewModel = new List<FeeDueDetailMonthlyClosingViewModel>();
        }


        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<FeeDueDetailMonthlyClosingViewModel> FeeDueDetailMonthlyClosingViewModel { get; set; }

    }
}
