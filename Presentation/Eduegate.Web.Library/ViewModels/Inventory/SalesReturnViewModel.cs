using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class SalesReturnViewModel : BaseMasterViewModel
    {
        public SalesReturnViewModel()
        {
            MasterViewModel = new SalesReturnMasterViewModel();
            DetailViewModel = new List<SalesReturnDetailViewModel>() { new SalesReturnDetailViewModel() };
        }

        public SalesReturnMasterViewModel MasterViewModel { get; set; }
        public List<SalesReturnDetailViewModel> DetailViewModel { get; set; }
    }
}
