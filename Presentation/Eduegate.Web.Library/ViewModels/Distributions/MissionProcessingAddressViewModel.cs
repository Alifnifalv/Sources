using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliveryDetails", "Model.MasterViewModel.Address")]
    public class MissionProcessingAddressViewModel : BaseMasterViewModel
    {

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Delivery Address")]
        public string DeliveryAddress { get; set; }        
        
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Mobile No")]
        public string MobileNo { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Landline No")]
        public string LandLineNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Special Instructions")]
        public string SpecialInstructions { get; set; }
        public long OrderContactMapID { get; set; }
    }
}
