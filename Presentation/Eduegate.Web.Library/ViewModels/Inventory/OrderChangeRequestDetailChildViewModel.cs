using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "detail.ChildGrid")]
    public class OrderChangeRequestDetailChildViewModel : BaseMasterViewModel
    {
        public OrderChangeRequestDetailChildViewModel()
        {
            Unit = "Pcs";
            this.IsHierarchicalGrid = false;
        }

        public long TransactionHead { get; set; }

        public long TransactionDetailID { get; set; }
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        //[DisplayName("Sr.No")]
        //public long SerialNo { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown, "large-col-width", "ng-change='ExchangeActionChange()'")]
        //[DisplayName("Action")]
        //[LookUp("LookUps.Action")]

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [Select2("Branch", "String", false, "OnChangeSelect2", false)]
        [DisplayName("Action")]
        [LookUp("LookUps.Action")]
        public KeyValueViewModel Action { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false)]
        [DisplayName("Product")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        public KeyValueViewModel SKUID { get; set; }
        public string BarCode { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-large-col-width")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        [DisplayName("Quantity")]
        public double Quantity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width", "initialvalue='Pcs'")]
        [DisplayName("Unit")]
        public string Unit { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright", "initialvalue=0")]
        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }
        public long? UnitID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [DisplayName("Amount")]
        public double Amount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "large-col-width textright")]
        [DisplayName("Remark")]
        public string Remark { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width", "ng-click='InsertGridRow($index+1, childGrid[0], childGrid)'")]
        //[DisplayName("+")]
        //public string Add { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width", "ng-click='RemoveGridRow($index, childGrid[0], childGrid)'")]
        //[DisplayName("-")]
        //public string Remove { get; set; }

        public long ParentDetailID { get; set; }
    }
}
