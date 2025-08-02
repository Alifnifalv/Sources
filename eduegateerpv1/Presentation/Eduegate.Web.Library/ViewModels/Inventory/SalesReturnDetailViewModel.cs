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
    public class SalesReturnDetailViewModel : BaseMasterViewModel
    {
        public SalesReturnDetailViewModel()
        {
            Unit = new KeyValueViewModel();
            IsScreen = "SalesReturn";
        }
        public string IsScreen { get; set; }
        public long TransactionHead { get; set; }
        public long TransactionDetailID { get; set; }
        public long? UnitGroupID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false)]
        [CustomDisplay("Item")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        public KeyValueViewModel SKUID { get; set; }
        public string BarCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Item Code")]
        public string ProductCode { get; set; }
        public string PartNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "ex-large-col-width")]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("AvailableQuantity")]
        public double AvailableQuantity { get; set; }


        [Required]
        [RegularExpression("^[1-9][0-9]*$",/*"[0-9]{1,}",*/ ErrorMessage = "Invalid format")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright serialnum-wrap", "ng-blur='BringPopup(detail)' ng-change='UpdateDetailGridValues(''@property.Name'', detail, {{$index + 1}})'", Attributes = "ng-disabled=!detail.SKUID.Key")]
        [CustomDisplay("Quantity")]
        public decimal? Quantity { get; set; }


        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "small-col-width", "initialvalue='Pcs'")]
        //[CustomDisplay("Unit")]
        //public string Unit { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Unit", "Numeric", false, "OnUnitChange(detail)", false, "ng-click=LoadUnit($index)")]
        [CustomDisplay("Unit")]
        [LookUp("LookUps.Unit")]
        public KeyValueViewModel Unit { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Fraction")]
        public double Fraction { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright", "ng-blur=GetPurchaseLocalAmount(detail)")]
        //[CustomDisplay("Foreign Rate")]
        //public decimal? ForeignRate { get; set; } = 0;


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("Rate")]
        public double UnitPrice { get; set; }
        public long UnitID { get; set; }


        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        //[CustomDisplay("Foreign Amount")]
        //public decimal? ForeignAmount { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("Amount")]
        public double Amount { get; set; }

        public Nullable<decimal> ExchangeRate { get; set; }        //public List<ProductSKUDetailsViewModel> SKUDetails { get; set; }
     
        public List<KeyValueViewModel> UnitDTO { get; set; }
        public List<UnitsViewModel> UnitList { get; set; }
        //[ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        //[Select2("CostCenter", "Numeric", false)]
        //[CustomDisplay("CostCenter")]
        //[LookUp("LookUps.CostCenterDetails")]
        //public KeyValueViewModel CostCenter { get; set; }
        public int? CostCenterID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
