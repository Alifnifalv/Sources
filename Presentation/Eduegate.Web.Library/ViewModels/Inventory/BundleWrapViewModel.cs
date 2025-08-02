using System.Collections.Generic;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class BundleWrapViewModel : BaseMasterViewModel
    {
        public BundleWrapViewModel()
        {
            MasterViewModel = new BundleWrapMasterViewModel();
            DetailViewModel = new List<BundleWrapDetailViewModel>() { new BundleWrapDetailViewModel() };
        }

        public BundleWrapMasterViewModel MasterViewModel { get; set; }
        public List<BundleWrapDetailViewModel> DetailViewModel { get; set; }
    }
}
