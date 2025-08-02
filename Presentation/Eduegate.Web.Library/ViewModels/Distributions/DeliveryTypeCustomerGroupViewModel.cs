using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerGroupCharge", "CRUDModel.ViewModel.CustomerGroupCharge")]
    [DisplayName("Delivery Charges")]
    public class DeliveryTypeCustomerGroupViewModel : BaseMasterViewModel
    {
        public DeliveryTypeCustomerGroupViewModel()
        {
            Charges = new List<CustomerGroupDeliveryChargeViewModel>() { new  CustomerGroupDeliveryChargeViewModel()} ;
        }       

        [ControlType(Framework.Enums.ControlTypes.Grid, "onecol-header-left")]
        [DisplayName("")]
        public List<CustomerGroupDeliveryChargeViewModel> Charges { get; set; }
    }
}
