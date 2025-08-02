using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class FOCSalesViewModel : BaseMasterViewModel
    {
        public FOCSalesViewModel()
        {
            MasterViewModel = new FOCSalesMasterViewModel();
            DetailViewModel = new List<FOCSalesDetailViewModel>() { new FOCSalesDetailViewModel() };
        }

        public FOCSalesMasterViewModel MasterViewModel { get; set; }
        public List<FOCSalesDetailViewModel> DetailViewModel { get; set; }
    }
}
