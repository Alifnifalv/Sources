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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ItemList", "CRUDModel.ViewModel.ItemList")]
    [DisplayName("Item List")]
    public class RFQComparisonDetailViewModel : BaseMasterViewModel
    {
        public RFQComparisonDetailViewModel()
        {
            SKUDetails = new List<ProductSKUDetailsViewModel>();
        }

        public long DetailIID { get; set; }
        public long? HeadID { get; set; }
        public long? SupplierID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Supplier")]
        public string Supplier { get; set; }  
        
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("QT.No")]
        public string QuotationNo { get; set; } 

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2,"disabled == true")]
        [Select2("SKUID", "String", false)]
        [CustomDisplay("Item")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        public KeyValueViewModel SKUID { get; set; }
        public string BarCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Item Code")]
        public string ProductCode { get; set; }
        public string PartNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Description")]
        public string Description { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Quantity")]
        public decimal Quantity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Unit")]
        public string Unit { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("Price")]
        public double UnitPrice { get; set; }
        public long UnitID { get; set; }

        public decimal? Fraction { get; set; }
        public decimal? ForeignRate { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [CustomDisplay("Amount")]
        public double Amount { get; set; }

        public List<ProductSKUDetailsViewModel> SKUDetails { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.ViewModel.ItemList[0], CRUDModel.ViewModel.ItemList)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
