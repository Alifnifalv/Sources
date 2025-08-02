using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Warehouses
{
    [Order(1)]
    public class JobPickingDetailViewModel : JobOperationDetailViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright serialnum-wrap")]
        [Order(5)]
        [DisplayName("Quantity")]
        public Nullable<decimal> VerifyQuantity { get; set; }

        [Required]
        [DisplayName("Bar code#")]
        [ControlType(Framework.Enums.ControlTypes.LabelWithTextBoxAndUpdateVerifyWithSave, "ex-large-col-width smalltextbox", "ng-keyup=\"VerifyProductBarcode(detail)\" ng-disabled='Model.IsLoginUserJob ?  true: false' ", "BarCode", "ng-click=\"SaveSKUBarCode(detail)\"")]
        [Order(6)]
        public string VerifyBarCode { get; set; }
        
        [Required]
        [DisplayName("Location")]
        [ControlType(Framework.Enums.ControlTypes.LabelWithTextBoxAndUpdateVerifyWithSave, "ex-large-col-width", "ng-keyup=\"VerifyLocationBarcode(detail)\" ng-disabled='Model.IsLoginUserJob ?  true: false' ", "LocationBarcode", "ng-click=\"SaveSKULocation(detail)\" ng-disabled='Model.IsLoginUserJob ?  true: false' ng-class=\"{'disabled': Model.IsLoginUserJob}\" ", 
            optionalAttribs3: "ng-show='detail.LocationBarcode.length > 0'")]
        [Order(7)]
        public string VerifyLocation { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.YesNoCheckBox, "medium-col-width", "ng-disabled='Model.IsLoginUserJob ?  true: false'")]
        [Order(8)]
        [DisplayName("Remaining Quantity")]
        public bool IsRemainingQuantity { get; set; }
    }
}
