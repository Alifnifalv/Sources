using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.CustomerService
{
    public class JobDetailViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Grid, "","","","",false,"","", true)]
        [DisplayName("")]
        public List<RepairOrderDetailViewModel> Details { get; set; }

        public JobDetailViewModel()
        {
            Details = new List<RepairOrderDetailViewModel>() { new RepairOrderDetailViewModel() };
        }
    }
}
