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
    public class StockVerificationDetailViewModel : BaseMasterViewModel
    {
        public StockVerificationDetailViewModel()
        {
            PhysicalQuantity = 0;
        }

        public long DetailIID { get; set; }

        public long? HeadID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        //[DisplayName("IsRowSelected")]
        //public bool IsRowSelected { get; set; }
        //public bool SelectAll { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false)]
        [DisplayName("Item")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        public KeyValueViewModel SKUID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("ItemCode")]
        public string ProductCode { get; set; }
        public KeyValueViewModel ProductSKU { get; set; }
        public long? ProductID { get; set; }
        public string BarCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Description")]
        public string Description { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        ////[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright", "initialvalue=0")]
        //[DisplayName("Unit Price")]
        //public double UnitPrice { get; set; }
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        //[DisplayName("Amount")]
        //public double Amount { get; set; }
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright serialnum-wrap")]
        //[DisplayName("Quantity")]
        //public double StockQuantity { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        //[DisplayName("Book Stock")]
        public decimal? AvailableQuantity { get; set; }
        public double Quantity { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width")]
        [DisplayName("Physical Stock")]
        public decimal? PhysicalQuantity { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        //[DisplayName("Book Quantity")]
        public double BookQuantity { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        //[DisplayName("Diff Quantity")]
        public double DifferenceQuantity { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        //[DisplayName("Physical Value")]
        public double PhysicalValue { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        //[DisplayName("Book Value")]
        public double BookValue { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        //[DisplayName("Diff Value")]
        public double DifferenceValue { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "large-col-width")]
        [DisplayName("Remarks")]
        public string Remark { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.DetailViewModel, CRUDModel.Model.DetailViewModel)'")]
        //[DisplayName("+")]
        //public string Add { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        //[DisplayName("-")]
        //public string Remove { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}
