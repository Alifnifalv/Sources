using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerDtails", "Model.MasterViewModel.OrderInfo.CustomerDetail", "class='alignleft two-column-header'")]
    [DisplayName("Customer")]
    public class CustomerDetailViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Customer Name : ")]
        public string CustomerName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Customer Email : ")]
        public string CustomerEmail { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Customer Number : ")]
        public string CustomerNumber { get; set; }
    }
}
