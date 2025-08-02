using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AreaMap", "CRUDModel.ViewModel.AreaMap")]
    [DisplayName("Area")]
    public class DeliveryTypeAreaViewModel : BaseMasterViewModel
    {
        public DeliveryTypeAreaViewModel()
        {
            AreaDetails = new List<DeliveryAreaDetailViewModel>() { new DeliveryAreaDetailViewModel() };
        }       

        [ControlType(Framework.Enums.ControlTypes.Grid, "onecol-header-left")]
        [DisplayName("")]
        public List<DeliveryAreaDetailViewModel> AreaDetails { get; set; }
    }
}
