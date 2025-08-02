using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class BranchTransferViewModel : BaseMasterViewModel
    {
        public BranchTransferViewModel()
        {
            MasterViewModel = new BranchTransferMasterViewModel();
            DetailViewModel = new List<BranchTransferDetailViewModel>() { new BranchTransferDetailViewModel() };
        }

        public BranchTransferMasterViewModel MasterViewModel { get; set; }
        public List<BranchTransferDetailViewModel> DetailViewModel { get; set; }
    }
}
