using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Eduegate.Web.Library.ViewModels.Catalog
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Product", "CRUDModel.ViewModel.ProductBundles")]
    [DisplayName("Bundle Details")]
    public class ProductBundleviewModel : BaseMasterViewModel
    {
        public ProductBundleviewModel()
        {
            FromProduct = new KeyValueViewModel();
        }
        public long BundleIID { get; set; }
        public long? ToProductID { get; set; }


        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Products", "Numeric", false, "OnProductBundleSelect($event, $element, gridModel)")]
        [LookUp("LookUps.Products")]
        [CustomDisplay("Product")]
        public KeyValueViewModel FromProduct { get; set; }
        public long? FromProductID { get; set; }

        public long? FromProductSKUMapID { get; set; }

        public long? ToProductSKUMapID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "ng-change='UpdateDetailGridValues('Quantity', gridModel)'")]
        [MaxLength(18)]
        [CustomDisplay("Quantity")]
        public decimal? Quantity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [CustomDisplay("AvailableQuantity")]
        public decimal? AvailableQuantity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Cost Price/Item")]
        public decimal? ItemCostPrice { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)] 
        //[DisplayName("Selling Price/Item")]
        public decimal? ItemSellingPrice { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("CostPrice")]
        public decimal? CostPrice { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("SellingPrice")]
        public decimal? SellingPrice { get; set; }

        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.ProductBundles[0], CRUDModel.ViewModel.ProductBundles)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.ProductBundles[0],CRUDModel.ViewModel.ProductBundles)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}