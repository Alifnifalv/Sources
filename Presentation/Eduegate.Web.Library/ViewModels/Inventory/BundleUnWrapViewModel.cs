using System.Collections.Generic;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class BundleUnWrapViewModel : BaseMasterViewModel
    {
        public BundleUnWrapViewModel()
        {
            MasterViewModel = new BundleUnWrapMasterViewModel();
            DetailViewModel = new List<BundleUnWrapDetailViewModel>() { new BundleUnWrapDetailViewModel() };
        }

        public BundleUnWrapMasterViewModel MasterViewModel { get; set; }
        public List<BundleUnWrapDetailViewModel> DetailViewModel { get; set; }
    }
}
