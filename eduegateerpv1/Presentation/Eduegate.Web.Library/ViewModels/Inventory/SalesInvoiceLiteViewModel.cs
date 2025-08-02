using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class SalesInvoiceLiteViewModel : BaseMasterViewModel
    {
        public SalesInvoiceLiteViewModel()
        {
            MasterViewModel = new SalesInvoiceLiteMasterViewModel();
            DetailViewModel = new List<SalesInvoiceLiteDetailViewModel>() { new SalesInvoiceLiteDetailViewModel() };
        }

        public SalesInvoiceLiteMasterViewModel MasterViewModel { get; set; }
        public List<SalesInvoiceLiteDetailViewModel> DetailViewModel { get; set; }
    }
}
