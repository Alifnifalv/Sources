using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.CustomerService
{
    public class RepariOrderSummaryViewModel : BaseMasterViewModel
    {
        [Order(1)]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "{{CRUDModel.Model.MasterViewModel.RONO}}")]
        [DisplayName("Order No")]
        public string RONO { get; set; }
    }
}
