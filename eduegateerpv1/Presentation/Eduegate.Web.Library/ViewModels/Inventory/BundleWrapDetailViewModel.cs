using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Domain;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.DetailViewModel")]
    public class BundleWrapDetailViewModel : BaseMasterViewModel
    {
        public BundleWrapDetailViewModel()
        {
            //SKUDetails = new List<ProductSerialMapViewModel>() { new ProductSerialMapViewModel() };
            //Unit = "Pcs";
            Unit = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_UNIT");
        }
        public long TransactionHead { get; set; }
        public long TransactionDetailID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false, "OnBundleSelect($event, $element, detail)")]
        [CustomDisplay("Product")]
        [LookUp("LookUps.BundleProduct")]
        //[LazyLoad("", "Catalogs/ProductSKU/ProductBundleSearch", "LookUps.ProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        public KeyValueViewModel SKUID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("PartNo")]
        public string PartNo { get; set; }

        public string BarCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Avalaible Quantity")]

        public string AvalaibleQuantity { get; set; }

        [Required]
        [RegularExpression("[0-9]{1,}", ErrorMessage = "Invalid format")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        //[MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [CustomDisplay("Quantity")]
        public double Quantity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "small-col-width textright"/*, "initialvalue='Pcs'"*/)]
        [LookUp("LookUps.Unit")]
        [CustomDisplay("Unit")]
        public string Unit { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright", "initialvalue=0")]
        [CustomDisplay("UnitCostPrice")]
        public double UnitPrice { get; set; }
       

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("Amount")]
        public double Amount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        //[DisplayName("+")]
        //public string Add { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        //[DisplayName("-")]
        //public string Remove { get; set; }
    }
}
