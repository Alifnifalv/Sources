using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory.Purchase
{
    public class PurchaseQuotationViewModel : BaseMasterViewModel
    {
        public PurchaseQuotationViewModel()
        {
            MasterViewModel = new PurchaseQuotationMasterViewModel();
            DetailViewModel = new List<PurchaseQuotationDetailViewModel>() { new PurchaseQuotationDetailViewModel() { } };
        }

        public PurchaseQuotationMasterViewModel MasterViewModel { get; set; }
        public List<PurchaseQuotationDetailViewModel> DetailViewModel { get; set; }
    }
}
