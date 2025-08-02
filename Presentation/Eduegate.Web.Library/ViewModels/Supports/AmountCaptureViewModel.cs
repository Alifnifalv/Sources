using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Supports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "6", "CRUDModel.ViewModel.ActionTab.AmountCapture")]
    [DisplayName("Amount Capture")]
    public class AmountCaptureViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Card Type")]
        public string CardType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Card Number")]
        public string CardNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Date")]
        public string AmountCaptureDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Bank Name")]
        public string BankName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Amount")]
        public string CapturedAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Amount Received")]
        [Select2("AmountReceived", "Numeric", false)]
        [LookUp("LookUps.AmountReceived")]
        public KeyValueViewModel AmountReceived { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Action")]
        [Select2("AmountCapturedAction", "Numeric", false, optionalAttribute1: "ng-show='CRUDModel.ViewModel.ActionTab.AmountCapture.AmountReceived.Key'")]
        [LookUp("LookUps.AmountCapturedAction")]
        public KeyValueViewModel AmountCapturedAction { get; set; }
    }
}
