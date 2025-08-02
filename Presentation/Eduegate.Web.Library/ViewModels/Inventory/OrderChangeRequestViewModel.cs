using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class OrderChangeRequestViewModel : BaseMasterViewModel
    {
        public OrderChangeRequestViewModel()
        {
            MasterViewModel = new OrderChangeRequestMasterViewModel();
            DetailViewModel = new List<OrderChangeRequestDetailViewModel>() { new OrderChangeRequestDetailViewModel() };
        }

        public OrderChangeRequestMasterViewModel MasterViewModel { get; set; }
        public List<OrderChangeRequestDetailViewModel> DetailViewModel { get; set; }
    }
}
