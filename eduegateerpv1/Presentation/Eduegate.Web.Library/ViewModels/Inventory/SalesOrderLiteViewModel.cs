using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class SalesOrderLiteViewModel : BaseMasterViewModel
    {
        public SalesOrderLiteViewModel()
        {
            MasterViewModel = new SalesOrderLiteMasterViewModel();
            DetailViewModel = new List<SalesOrderLiteDetailViewModel>() { new SalesOrderLiteDetailViewModel() };
        }

        public SalesOrderLiteMasterViewModel MasterViewModel { get; set; }
        public List<SalesOrderLiteDetailViewModel> DetailViewModel { get; set; }
    }
}
