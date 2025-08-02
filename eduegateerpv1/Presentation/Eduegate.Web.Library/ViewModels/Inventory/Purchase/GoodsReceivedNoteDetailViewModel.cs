using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory.Purchase
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.DetailViewModel")]
    public class GoodsReceivedNoteDetailViewModel : BaseMasterViewModel
    {
        public GoodsReceivedNoteDetailViewModel()
        {
            SKUDetails = new List<ProductSKUDetailsViewModel>() { new ProductSKUDetailsViewModel() };
            IsScreen = "GRN";
            Unit = new KeyValueViewModel();
            ForeignRate = 0;
            IsDisableSelect2 = false;
        }
        public long TransactionHead { get; set; }
        public long TransactionDetailID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("Sr.No")]
        public long SerialNo { get; set; }
        public string IsScreen { get; set; }

        public bool? IsDisableSelect2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false, optionalAttribute1: "ng-disabled=CRUDModel.Model.DetailViewModel.IsDisableSelect2 == true")]
        [CustomDisplay("Item")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        public KeyValueViewModel SKUID { get; set; }
        public string BarCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Item Code")]
        public string ProductCode { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        //[CustomDisplay("PartNo#")]
        public string PartNo { get; set; }

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

        [Required]
        [RegularExpression("[0-9]{1,}", ErrorMessage = "Invalid format")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        [CustomDisplay("Quantity")]
        public double Quantity { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Unit", "Numeric", false, "OnUnitChange(detail)", false, "ng-click=LoadUnit($index)")]
        [CustomDisplay("Unit")]
        [LookUp("LookUps.Unit")]
        public KeyValueViewModel Unit { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Fraction")]
        public double Fraction { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright", "ng-blur=GetPurchaseLocalAmount(detail)")]
        [CustomDisplay("F Rate")]
        public float? ForeignRate { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("L Rate")]
        public double UnitPrice { get; set; }
        public long UnitID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("F Amount")]
        public decimal? ForeignAmount { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("L Amount")]
        public double Amount { get; set; }

        public Nullable<decimal> ExchangeRate { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        //[Select2("CostCenter", "Numeric", false)]
        //[CustomDisplay("CostCenter")]
        //[LookUp("LookUps.CostCenterDetails")]
        //public KeyValueViewModel CostCenter { get; set; }
        public int? CostCenterID { get; set; }

        public List<ProductSKUDetailsViewModel> SKUDetails { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.DetailViewModel, CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        public long? UnitGroupID { get; set; }

        public List<KeyValueViewModel> UnitDTO { get; set; }

        public List<UnitsViewModel> UnitList { get; set; }


    }
}
