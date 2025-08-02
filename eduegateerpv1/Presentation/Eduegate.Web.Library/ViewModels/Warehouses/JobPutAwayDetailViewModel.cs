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
    public class JobPutAwayDetailViewModel : JobOperationDetailViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright serialnum-wrap")]
        [Order(5)]
        [DisplayName("Quantity")]
        public Nullable<decimal> VerifyQuantity { get; set; }
        
        [Required]
        [DisplayName("Bar code#")]
        [ControlType(Framework.Enums.ControlTypes.LabelWithTextBoxAndUpdateVerifyWithSave, "medium-col-width smalltextbox", "ng-keyup=\"VerifyProductBarcode(detail)\"", 
            "BarCode", "ng-click=\"SaveSKUBarCode(detail)\"")]
        [Order(6)]
        public string VerifyBarCode { get; set; }

        [Required]
        [DisplayName("Location")]
        [ControlType(Framework.Enums.ControlTypes.LabelWithTextBoxAndUpdateVerifyWithSave, "ex-large-col-width", "ng-keyup=\"VerifyLocationBarcode(detail)\"",
            "LocationBarcode", "ng-click=\"SaveSKULocation(detail)\"")]
        [Order(7)]
        public string VerifyLocation { get; set; }
    }
}
