using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.DetailViewModel")]
    public class BranchTransferDetailViewModel : BaseMasterViewModel
    {

        public BranchTransferDetailViewModel()
        {
            IsScreen = "BranchTransfer";
            Unit = new KeyValueViewModel();
        }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        [CustomDisplay("IsRowSelected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }
        public long TransactionHead { get; set; }
        public long TransactionDetailID { get; set; }
        public string IsScreen { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false)]
        [CustomDisplay("Product")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        public KeyValueViewModel SKUID { get; set; }
        public string BarCode { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-large-col-width")]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "ex-large-col-width")]
        [CustomDisplay("AvailableQuantity")]
        public long AvailableQuantity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Unit", "Numeric", false, "OnUnitChange(detail)", false)]
        [CustomDisplay("Unit")]
        [LookUp("LookUps.Unit")]
        public KeyValueViewModel Unit { get; set; }

        public long UnitID { get; set; }

        public long? UnitGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Fraction")]
        public double Fraction { get; set; }

        [Required]
        [RegularExpression("[0-9]{1,}", ErrorMessage = "Invalid format")]
        [ControlType(Framework.Enums.ControlTypes.TextBoxWithPopup, "medium-col-width textright serialnum-wrap", "ng-blur='BringPupup(detail)' ng-change='UpdateDetailGridValues(''@property.Name'', detail, {{$index + 1}})'")]
        [CustomDisplay("Quantity")]
        public double Quantity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
