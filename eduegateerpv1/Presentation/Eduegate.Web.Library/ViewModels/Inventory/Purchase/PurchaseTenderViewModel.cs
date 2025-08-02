using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory.Purchase
{
    public class PurchaseTenderViewModel : BaseMasterViewModel
    {
        public PurchaseTenderViewModel()
        {
            MasterViewModel = new PurchaseTenderMasterViewModel();
            DetailViewModel = new List<PurchaseTenderDetailViewModel>() { new PurchaseTenderDetailViewModel() { } };
        }

        public PurchaseTenderMasterViewModel MasterViewModel { get; set; }
        public List<PurchaseTenderDetailViewModel> DetailViewModel { get; set; }
    }
}
