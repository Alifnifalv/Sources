using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeePaymentMap", "CRUDModel.ViewModel.FeePaymentMap")]
    [DisplayName("Fee Payment")]
    public class FinalSettlementPaymentModeMapViewModel : BaseMasterViewModel
    {
        public FinalSettlementPaymentModeMapViewModel()
        {
            IsTDateDisabled = false;
            PaymentMode = new KeyValueViewModel();
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("PaymentMode")]
        [Select2("PaymentMode", "Numeric", false, "PaymodeChanges($event, $index, gridModel)", false, "ng-click=LoadPaymentMode($index)")]
        [LookUp("LookUps.PaymentModesExclude")]
        public KeyValueViewModel PaymentMode { get; set; }
        public int PaymentModeId { get; set; }
        public bool IsTDateDisabled { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", attribs: "ng-disabled=gridModel.IsTDateDisabled")]

        [CustomDisplay("Date")]
        public string TDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", attribs: "ng-disabled=gridModel.IsTDateDisabled")]
        [MaxLength(25, ErrorMessage = "Maximum Length should be within 25!")]
        [CustomDisplay("ReferenceNo")]
        public string ReferenceNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [CustomDisplay("Amount")]
        public decimal? PayAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.FeePaymentMap[0], CRUDModel.ViewModel.FeePaymentMap)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.FeePaymentMap[0],CRUDModel.ViewModel.FeePaymentMap)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}