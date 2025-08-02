using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "PaymentDetails", "Model.MasterViewModel.OrderInfo.PaymentDetail", "class='alignleft two-column-header'")]
    [DisplayName("Payment")]
    public class PaymentDetailViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Payments")]
        public string PaymentMethod { get; set; }
    }
}
