using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Warehouses;

namespace Eduegate.Web.Library.ViewModels.Warehouses
{
    public class JobEntryDetailViewModel : BaseMasterViewModel
    {
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
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false)]
        [DisplayName("Product")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        public KeyValueViewModel SKUID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-large-col-width")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright serialnum-wrap")]
        [DisplayName("Quantity")]
        public double Quantity { get; set; }

        public string LocationName { get; set; }

        public Nullable<int> LocationID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright serialnum-wrap")]
        [DisplayName("Location")]
        public string LocationBarcode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright serialnum-wrap")]
        [DisplayName("Bar code#")]
        public string BarCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Image, "medium-col-width textright thumbnailview")]
        [DisplayName("Product Image")]
        public string ProductImage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.DetailViewModel, CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
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
