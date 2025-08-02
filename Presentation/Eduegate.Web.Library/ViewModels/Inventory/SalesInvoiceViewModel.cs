using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class SalesInvoiceViewModel : BaseMasterViewModel
    {
        public SalesInvoiceViewModel()
        {
            MasterViewModel = new SalesInvoiceMasterViewModel();
            DetailViewModel = new List<SalesInvoiceDetailViewModel>() { new SalesInvoiceDetailViewModel() };
        }

        public SalesInvoiceMasterViewModel MasterViewModel { get; set; }
        public List<SalesInvoiceDetailViewModel> DetailViewModel { get; set; }
    }
}
