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
    public class PurchaseRequestDetailViewModel : BaseMasterViewModel
    {
        public PurchaseRequestDetailViewModel()
        {
            SKUDetails = new List<ProductSerialMapViewModel>() { new ProductSerialMapViewModel() };
            IsScreen = "PurchaseRequest";
            Unit = new KeyValueViewModel();
            UnitList = new List<UnitsViewModel>() { new UnitsViewModel() }; 
            UnitDTO = new List<KeyValueViewModel>() { new KeyValueViewModel() };
        }
        public string IsScreen { get; set; }
        public int? DocumentTypeID { get; set; }
        public long TransactionHead { get; set; }
        public long TransactionDetailID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("Sr.No")]
        public long SerialNo { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.YesNoCheckBox, "small-col-width")]
        //[CustomDisplay("Is Ordered Before")]
        public bool? HasItemOrderedBefore { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false)]
        [CustomDisplay("Item")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        public KeyValueViewModel SKUID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Item Code")]
        public string ProductCode { get; set; }
        public string PartNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Description")]
        public string Description { get; set; }


        [Required]
        [RegularExpression("^[1-9][0-9]*$",/*"[0-9]{1,}",*/ ErrorMessage = "Invalid format")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright serialnum-wrap ng-change='UpdateDetailGridValues(''@property.Name'', detail, {{$index + 1}})'")]
        [CustomDisplay("Quantity")]
        public decimal? Quantity { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Unit", "Numeric", false, "OnUnitChange(detail)", false, "ng-click=LoadUnit($index)")]
        [CustomDisplay("Unit")]
        [LookUp("LookUps.Unit")]
        public KeyValueViewModel Unit { get; set; }


        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width textright")]
        [CustomDisplay("UnitPrice")]
        public decimal? UnitPrice { get; set; }
        public long UnitID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        [CustomDisplay("Expected Unit Price")]
        public decimal? ExpectedUnitPrice { get; set; }

        public List<ProductSerialMapViewModel> SKUDetails { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.DetailViewModel, CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        public long? UnitGroupID { get; set; }

        public List<KeyValueViewModel> UnitDTO { get; set; }

        public List<UnitsViewModel> UnitList { get; set; }

        public bool? IsSerialNumber { get; set; }

        public decimal? Amount { get; set; }

        public decimal? Fraction { get; set; }
        
        public decimal? ForeignRate { get; set; } 
    }
}
