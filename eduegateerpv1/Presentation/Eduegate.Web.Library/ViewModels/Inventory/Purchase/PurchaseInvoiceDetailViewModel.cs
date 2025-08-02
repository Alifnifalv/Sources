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
    public class PurchaseInvoiceDetailViewModel : BaseMasterViewModel
    {
        public PurchaseInvoiceDetailViewModel()
        {
            SKUDetails = new List<ProductSKUDetailsViewModel>() { new ProductSKUDetailsViewModel()};
            //Unit = "Pcs";
            TaxPercentage = 0;
            SKUID = new KeyValueViewModel();
            IsScreen = "PurchaseInvoice";
            Unit = new KeyValueViewModel(); 
            IsDisableSelect2 = false;
        }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox, "check-box", "ng-model='detail.IsRowSelected'")]
        //[DisplayName("IsRowSelected")]
        public bool IsRowSelected { get; set; }     
        public string IsScreen { get; set; }
        public bool SelectAll { get; set; }
        public long TransactionHead { get; set; }
        public long TransactionDetailID { get; set; }

        public bool? IsDisableSelect2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false, optionalAttribute1: "ng-disabled=CRUDModel.Model.DetailViewModel.IsDisableSelect2 == true")]
        [CustomDisplay("Item")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU")]
        public KeyValueViewModel SKUID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Item Code")]
        public string ProductCode { get; set; }
        public string PartNo { get; set; }

        public string BarCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "ex-large-col-width")]
        [CustomDisplay("Description")]
        public string Description { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.DatePicker, "medium-col-width")]
        //[CustomDisplay("WarrantyStartDate")]
        public string WarrantyStartDateString { get; set; }
        public DateTime? WarrantyStartDate { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.DatePicker, "medium-col-width")]
        //[CustomDisplay("WarrantyEndDate")]
        public string WarrantyEndDateString { get; set; }
        public DateTime? WarrantyEndDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("AvailableQuantity")]
        public double AvailableQuantity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, "medium-col-width", "ng-change='TemplateItemChange($event, $element, detail)'")]
        [LookUp("LookUps.TaxTemplates")]
        [CustomDisplay("Tax")]
        [HasClaim(HasClaims.HASVAT)]
        public string TaxTemplate { get; set; }

        public int? TaxTemplateID { get; set; }
        public decimal? TaxPercentage { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]*$",/*"[0-9]{1,}",*/ ErrorMessage = "Invalid format")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright serialnum-wrap", "ng-blur='BringPopup(detail)' ng-change='UpdateDetailGridValues(''@property.Name'', detail, {{$index + 1}})'", Attributes = "ng-disabled=!detail.SKUID.Key")]
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
        public Nullable<decimal> Fraction { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright", "ng-blur=GetPurchaseLocalAmount(detail)")]
        [CustomDisplay("F Rate")]
        public Nullable<decimal> ForeignRate { get; set; } = 0;


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("L Rate")]
        public Nullable<decimal> UnitPrice { get; set; }
        public long UnitID { get; set; }

       
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("F Amount")]
        public Nullable<decimal> ForeignAmount { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("L Amount")]
        public Nullable<decimal> Amount { get; set; }       


        //[ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        //[Select2("CostCenter", "Numeric", false)]
        //[CustomDisplay("Cost Center")]
        //[LookUp("LookUps.CostCenterDetails")]
        //public KeyValueViewModel CostCenter { get; set; }
        public int? CostCenterID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width", Attributes = "ng-bind='SetLandingCost(detail) | number'")]
        [CustomDisplay("LandingCost")]
        public Nullable<decimal> LandingCost { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("Last Cost Price")]
        public Nullable<decimal> LastCostPrice { get; set; }

        //[ControlType(Nullable<decimal>Framework.Enums.ControlTypes.HiddenWithLabel, "medium-col-width textright", "{{detail.TaxAmount=detail.Amount*(detail.TaxPercentage/100);detail.InclusiveTaxAmount=(!detail.HasTaxInclusive? 0 : detail.Amount*(detail.TaxPercentage/100));detail.ExclusiveTaxAmount=(detail.HasTaxInclusive? 0 : detail.Amount*(detail.TaxPercentage/100))}}")]
        //[CustomDisplay("TaxAmount")]
        //[HasClaim(HasClaims.HASVAT)]
        public double TaxAmount { get; set; }

        public List<ProductSKUDetailsViewModel> SKUDetails { get; set; }
        public bool IsQuantityPopup { get; set; }

        public bool IsSerialNumberOnPurchase { get; set; }
        public bool IsSerailNumberAutoGenerated { get; set; }
        public string ProductTypeName { get; set; }
        public int? ProductLength { get; set; }

        public decimal? InclusiveTaxAmount { get; set; }
        public decimal? ExclusiveTaxAmount { get; set; }
        public bool? HasTaxInclusive { get; set; }

        public long? UnitGroupID { get; set; }
        public float? ExchangeRate { get; set; }       

        public List<KeyValueViewModel> UnitDTO { get; set; }

        public List<UnitsViewModel> UnitList { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.DetailViewModel, CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
