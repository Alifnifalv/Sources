using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.DetailViewModel")]
    public class SalesOrderLiteDetailViewModel : BaseMasterViewModel
    {
        public SalesOrderLiteDetailViewModel()
        {
            SKUDetails = new List<ProductSerialMapViewModel>() { new ProductSerialMapViewModel() };
            Unit = new KeyValueViewModel();
            IsScreen = "SalesOrder";
        }

        public string IsScreen { get; set; }
        public long TransactionHead { get; set; }
        public long TransactionDetailID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "x-small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false)]
        [DisplayName("Item")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        public KeyValueViewModel SKUID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Item Code")]
        public string ProductCode { get; set; }
        public string PartNo { get; set; }

        public string BarCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "ex-large-col-width")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [DisplayName("Available Qty")]
        public double AvailableQuantity { get; set; }


        [Required]
        [RegularExpression("^[1-9][0-9]*$",/*"[0-9]{1,}",*/ ErrorMessage = "Invalid format")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright serialnum-wrap", "ng-blur='BringPopup(detail)' ng-change='UpdateDetailGridValues(''@property.Name'', detail, {{$index + 1}})'", Attributes = "ng-disabled=!detail.SKUID.Key")]
        [CustomDisplay("Quantity")]
        public decimal Quantity { get; set; }


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

        public Nullable<decimal> ExchangeRate { get; set; }

        public long? UnitGroupID { get; set; }

        public List<KeyValueViewModel> UnitDTO { get; set; }

        public List<UnitsViewModel> UnitList { get; set; }


        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        //[DisplayName("Delievry Charge")]
        //public Nullable<decimal> DeliveryCharge { get; set; }
        public List<ProductSerialMapViewModel> SKUDetails { get; set; }

        public Nullable<decimal> Weight { get; set; }

        [DisplayName("Warranty Date")]
        [HasClaim(HasClaims.HASWARRANTY)]
        public string WarrantyDate { get; set; }

        public bool? IsSerialNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
