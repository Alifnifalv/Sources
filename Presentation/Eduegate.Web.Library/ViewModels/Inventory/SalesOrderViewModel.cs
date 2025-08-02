using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class SalesOrderViewModel : BaseMasterViewModel
    {
        public SalesOrderViewModel()
        {
            MasterViewModel = new SalesOrderMasterViewModel();
            DetailViewModel = new List<SalesOrderDetailViewModel>() { new SalesOrderDetailViewModel() };
        }

        public SalesOrderMasterViewModel MasterViewModel { get; set; }
        public List<SalesOrderDetailViewModel> DetailViewModel { get; set; }
    }
}
