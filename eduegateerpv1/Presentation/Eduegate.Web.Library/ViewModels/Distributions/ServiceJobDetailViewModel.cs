using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    public class ServiceJobDetailViewModel : BaseMasterViewModel
    {
        [Order(0)]
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width statuscolumn", "ng-model='detail.IsRowSelected'")]
        [DisplayName("IsRowSelected")]
        public bool IsRowSelected { get; set; }

        public bool SelectAll { get; set; }
        public long TransactionHead { get; set; }
        public long JobEntryDetailIID { get; set; }
        public Nullable<long> JobEntryHeadID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        [Order(1)]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false)]
        [DisplayName("Product")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        [Order(2)]
        public KeyValueViewModel SKUID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-large-col-width")]
        [DisplayName("Description")]
        [Order(3)]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright serialnum-wrap")]
        [DisplayName("Quantity")]
        [Order(4)]
        public decimal? Quantity { get; set; }

        public string LocationName { get; set; }

        public Nullable<int> LocationID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright serialnum-wrap")]
        //[DisplayName("Location")]
        //public string LocationBarcode { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright serialnum-wrap")]
        //[DisplayName("Bar code#")]
        //public string BarCode { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Image, "medium-col-width textright thumbnailview")]
        //[DisplayName("Product Image")]
        //public string ProductImage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        [Order(5)]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        [Order(6)]
        public string Remove { get; set; }

        public Nullable<bool> IsQuantiyVerified { get; set; }
        public Nullable<bool> IsBarCodeVerified { get; set; }
        public Nullable<bool> IsLocationVerified { get; set; }
        public Nullable<int> JobStatusID { get; set; }
        public Nullable<decimal> ValidatedQuantity { get; set; }
        public string ValidatedLocationBarcode { get; set; }
        public string ValidatedPartNo { get; set; }
        public string ValidationBarCode { get; set; }
        public string Remarks { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        //public string TransactionNo { get; set; }
    }
}
