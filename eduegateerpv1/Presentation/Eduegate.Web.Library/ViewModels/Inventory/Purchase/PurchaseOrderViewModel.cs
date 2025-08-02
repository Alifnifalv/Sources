using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class PurchaseOrderViewModel : BaseMasterViewModel
    {
        public PurchaseOrderViewModel()
        {
            MasterViewModel = new PurchaseOrderMasterViewModel();
            DetailViewModel = new List<PurchaseOrderDetailViewModel>() { new PurchaseOrderDetailViewModel() { } };
        }

        public PurchaseOrderMasterViewModel MasterViewModel { get; set; }
        public List<PurchaseOrderDetailViewModel> DetailViewModel { get; set; }
    }
}
