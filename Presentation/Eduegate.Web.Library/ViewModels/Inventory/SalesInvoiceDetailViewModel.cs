using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.DetailViewModel")]
    public class SalesInvoiceDetailViewModel : BaseMasterViewModel
    {
        public SalesInvoiceDetailViewModel()
        {
            SKUDetails = new List<ProductSerialMapViewModel>() { new ProductSerialMapViewModel() };
            SKUID = new KeyValueViewModel();
            Unit = new KeyValueViewModel();
            IsScreen = "SalesInvoice";
        }

        public string IsScreen { get; set; }

        public long TransactionHead { get; set; }
        public long TransactionDetailID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DataPicker, "medium-col-width")]
        //[DisplayName("Product Search")]
        //[DataPicker("ProductAdvancedSearchView")]
        //public string ProductReferenceNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false)]
        [CustomDisplay("Item")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ActiveProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        public KeyValueViewModel SKUID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        //[CustomDisplay("PartNo#")]

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Item Code")]
        public string ProductCode { get; set; }
        public string PartNo { get; set; }

        public string BarCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "ex-large-col-width")]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "medium-col-width")]
        [CustomDisplay("WarrantyDate")]
        [HasClaim(HasClaims.HASWARRANTY)]
        public String WarrantyDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("AvailableQuantity")]
        public double? AvailableQuantity { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]*$",/*"[0-9]{1,}",*/ ErrorMessage = "Invalid format")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright serialnum-wrap", "ng-blur='BringPopup(detail)' ng-change='UpdateDetailGridValues(''@property.Name'', detail, {{$index + 1}})'", Attributes = "ng-disabled='!detail.SKUID.Key' ng-keyup='$event.keyCode == 13 && OnKeyPressSearchTextFocus()'")]
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
        //[CustomDisplay("Rate")]
        //public decimal? ForeignRate { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        [CustomDisplay("Rate")]
        public decimal? UnitPrice { get; set; }
        public long UnitID { get; set; }


        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        //[CustomDisplay("Foreign Amount")]
        //public decimal? ForeignAmount { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("Amount")]
        public decimal? Amount { get; set; }

        public Nullable<decimal> ExchangeRate { get; set; }

        public long? UnitGroupID { get; set; }

        public List<KeyValueViewModel> UnitDTO { get; set; }

        public List<UnitsViewModel> UnitList { get; set; }

        public bool IsOnEdit { get; set; } = false;
        //[ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        //[Select2("CostCenter", "Numeric", false)]
        //[CustomDisplay("CostCenter")]
        //[LookUp("LookUps.CostCenterDetails")]
        //public KeyValueViewModel CostCenter { get; set; }


        public int? CostCenterID { get; set; }

        public List<ProductSerialMapViewModel> SKUDetails { get; set; }

        public bool? IsSerialNumber { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        //[DisplayName("GST")]
        //[HasClaim(HasClaims.HASGST)]
        public double CGSTAmount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        //[DisplayName("SGST")]
        //[HasClaim(HasClaims.HASGST)]
        public double SGSTAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.DetailViewModel, CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}