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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "Model.DetailViewModel")]
    public class JobRecevingDetailViewModel: JobOperationDetailViewModel
    {
        [Required]
        [DisplayName("Quantity")]
        [ControlType(Framework.Enums.ControlTypes.LabelWithTextBox, "medium-col-width textright serialnum-wrap", "", "Quantity")]
        [Order(6)]
        public Nullable<decimal> VerifyQuantity { get; set; }

        [Required]
        [DisplayName("Price")]
        [ControlType(Framework.Enums.ControlTypes.LabelWithTextBox, "medium-col-width textright serialnum-wrap", "", "Price")]
        [Order(6)]
        public string VerifyPrice { get; set; }

        [Required]
        [DisplayName("Verify PartNo")]
        [ControlType(Framework.Enums.ControlTypes.LabelWithTextBoxAndUpdateVerifyWithSave, "medium-col-width serialnum-wrap", "ng-keyup=\"VerifyProductPartNo(detail)\" ng-disabled='Model.IsLoginUserJob ?  true: false' ", "PartNo", "ng-click=\"SaveSKUPartNo(detail)\" ng-disabled='Model.IsLoginUserJob ?  true: false' ng-class=\"{'disabled': Model.IsLoginUserJob}\" ")]
        [Order(3)]
        public string VerifyPartNo { get; set; }

        [Required]
        [DisplayName("BarCode")]
        [ControlType(Framework.Enums.ControlTypes.LabelWithTextBoxAndUpdateVerifyWithSave, "medium-col-width serialnum-wrap", "ng-keyup=\"VerifyProductBarCode(detail)\" ng-disabled='Model.IsLoginUserJob ?  true: false' ", "Barcode", "ng-click=\"SaveSKUBarCode(detail)\" ng-disabled='Model.IsLoginUserJob ?  true: false' ng-class=\"{'disabled': Model.IsLoginUserJob}\" ")]
        [Order(7)]
        public string VerifyBarCode { get; set; }
    }
}
