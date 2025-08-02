using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;


namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "DebitProductTransactionDetails", "CRUDModel.Model.DetailViewModel")]
    public class DebitProductDetailViewModel : BaseMasterViewModel
    {
        public DebitProductDetailViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        [DisplayName("IsRowSelected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }
        public long AccountTransactionDetailIID { get; set; }
        public Nullable<long> AccountTransactionHeadID { get; set; }

        public Nullable<long> ProductSKUId { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false, "ProductSKUCodeChange")]
        [DisplayName("Product")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        public KeyValueViewModel SKUID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [DisplayName("Description")]
        public string ProductSKUCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [DisplayName("Available Qty")]
        public Nullable<decimal> AvailableQuantity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("CurrentAvgCost")]
        public Nullable<decimal> CurrentAvgCost { get; set; }


        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be numeric")]        
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width", "ng-disabled='detail.SKUID.Key == null'", "", optionalAttribs: "AmountChange")]
        [DisplayName("Amount")]
        public double? Amount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("NewAvgCost")]
        public Nullable<decimal> NewAvgCost { get; set; }



        public decimal Currency { get; set; }
        public decimal ExchangeRate { get; set; }
        public Nullable<long> AccountID { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> CostCenterID { get; set; }


    }
}


