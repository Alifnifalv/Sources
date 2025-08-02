using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class SalesReturnRequestViewModel : BaseMasterViewModel
    {
        public SalesReturnRequestViewModel()
        {
            MasterViewModel = new SalesReturnRequestMasterViewModel();
            DetailViewModel = new List<SalesReturnRequestDetailViewModel>() { new SalesReturnRequestDetailViewModel() };
        }

        public SalesReturnRequestMasterViewModel MasterViewModel { get; set; }
        public List<SalesReturnRequestDetailViewModel> DetailViewModel { get; set; }
    }
}
