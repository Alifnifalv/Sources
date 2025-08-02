using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class BranchTransferRequestViewModel : BaseMasterViewModel
    {
        public BranchTransferRequestViewModel()
        {
            MasterViewModel = new BranchTransferRequestMasterViewModel();
            DetailViewModel = new List<BranchTransferRequestDetailViewModel>() {  new BranchTransferRequestDetailViewModel() };
        }

        public BranchTransferRequestMasterViewModel MasterViewModel { get; set; }
        public List<BranchTransferRequestDetailViewModel> DetailViewModel { get; set; }
    }
}
