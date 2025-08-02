using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.DetailViewModel")]
    public class DeliveryNoteDetailViewModel : BaseMasterViewModel
    {
        public DeliveryNoteDetailViewModel()
        {
            SKUDetails = new List<ProductSerialMapViewModel>() { new ProductSerialMapViewModel() };
            Unit = "Pcs";
        }
        public long TransactionHead { get; set; }
        public long TransactionDetailID { get; set; }
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

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Part No#")]
        public string PartNo { get; set; }

        public string BarCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "ex-large-col-width")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [DisplayName("Available Qty")]
        public double? AvailableQuantity { get; set; }

        [Required]
        [RegularExpression("[0-9]{1,}", ErrorMessage = "Invalid format")]
        [ControlType(Framework.Enums.ControlTypes.TextBoxWithPopup, "small-col-width textright serialnum-wrap", "ng-blur='BringPopup(detail)' ng-change='UpdateDetailGridValues(''@property.Name'', detail, {{$index + 1}})'")]
        [DisplayName("Quantity")]
        public double Quantity { get; set; }

        public List<ProductSerialMapViewModel> SKUDetails { get; set; }

        public bool? IsSerialNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width", "initialvalue='Pcs'")]
        [DisplayName("Unit")]
        public string Unit { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright", "initialvalue=0")]
        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }
        public long UnitID { get; set; }
        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker,"medium-col-width")]
        [DisplayName("Warranty Date")]
        [HasClaim(HasClaims.HASWARRANTY)]
        public String WarrantyDate { get; set; }        

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [DisplayName("Amount")]
        public double Amount { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        [DisplayName("GST")]
        [HasClaim(HasClaims.HASGST)]
        public double CGSTAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        [DisplayName("SGST")]
        [HasClaim(HasClaims.HASGST)]
        public double SGSTAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.DetailViewModel, CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}