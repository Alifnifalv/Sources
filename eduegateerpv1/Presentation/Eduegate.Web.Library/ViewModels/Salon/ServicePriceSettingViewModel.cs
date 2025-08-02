using Eduegate.Framework.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Saloon
{
    public class ServicePriceSettingViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("")]
        public List<ServicePriceViewModel> PriceDetails { get; set; }

        public ServicePriceSettingViewModel()
        {
            PriceDetails = new List<ServicePriceViewModel>() { new ServicePriceViewModel() };
        }
    }
}
