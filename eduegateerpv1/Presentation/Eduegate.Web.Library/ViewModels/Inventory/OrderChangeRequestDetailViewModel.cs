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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.DetailViewModel", colspan: "11")]
    public class OrderChangeRequestDetailViewModel : BaseMasterViewModel
    {
        public OrderChangeRequestDetailViewModel()
        {
            Unit = "Pcs";
            this.IsHierarchicalGrid = true;
            this.ChildGrid = new List<OrderChangeRequestDetailChildViewModel>() { new OrderChangeRequestDetailChildViewModel() };

        }

        public long TransactionHead { get; set; }
        public long TransactionDetailID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width", "`")]
        //[DisplayName("+")]
        //public string ExpandGrid { get; set; } 

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        //[Select2("SKUID", "String", false)]
        //[DisplayName("Product")]
        //[LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        //[QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width", "ng-bind='CRUDModel.Model.DetailViewModel.SKUID.Value'")]
        [DisplayName("Product")]
        public string SKUName { get; set; }

        public KeyValueViewModel SKUID { get; set; }
        public string BarCode { get; set; }
        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-large-col-width")]
        [DisplayName("Description")]
        public string Description { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [DisplayName("Quantity")]
        public double Quantity { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width", "initialvalue='Pcs'")]
        [DisplayName("Unit")]
        public string Unit { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright", "initialvalue=0")]
        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }
        public long UnitID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [DisplayName("Amount")]
        public double Amount { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width textright")]
        [DisplayName("Remarks")]
        public string Remark { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width", "ng-click='InsertGridRow($index+1, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        //[DisplayName("+")]
        //public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("")]
        public List<OrderChangeRequestDetailChildViewModel> ChildGrid { get; set; }



    }
}
