using System.Collections.Generic;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class SalesQuotationViewModel : BaseMasterViewModel
    {
        public SalesQuotationViewModel()
        {
            MasterViewModel = new SalesQuotationMasterViewModel();
            DetailViewModel = new List<SalesQuotationDetailViewModel>() { new SalesQuotationDetailViewModel() };
        }

        public SalesQuotationMasterViewModel MasterViewModel { get; set; }
        public List<SalesQuotationDetailViewModel> DetailViewModel { get; set; }
    }
}