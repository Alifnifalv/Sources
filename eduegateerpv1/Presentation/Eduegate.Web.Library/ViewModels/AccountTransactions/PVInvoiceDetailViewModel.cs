using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "PVInvoiceTransactionDetails", "CRUDModel.Model.DetailViewModel")]
    public class PVInvoiceDetailViewModel : BaseMasterViewModel
    {
        public PVInvoiceDetailViewModel()
        {

        }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        //[DisplayName("Is Row Selected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }
        public long AccountTransactionDetailIID { get; set; }
        public Nullable<long> AccountTransactionHeadID { get; set; }
        public long? PayableID { get; set; }
        public Nullable<int> CurrencyID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("InvoiceNumber")]
        public string InvoiceNumber { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width")]
        [CustomDisplay("Ref.No")]
        public string ReferenceNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("InvoiceAmount")]
        public double? InvoiceAmount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("ReturnAmount")]
        public double? ReturnAmount { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("PaymentDueDate")]
        public string PaymentDueDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("PaidAmount")]
        public double? PaidAmount { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("UnPaidAmount")]
        public double? UnPaidAmount { get; set; }


        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be numeric")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width")]
        [CustomDisplay("Amount")]
        public double? Amount { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        //[DisplayName("Currency")]
        //public string CurrencyName { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        //[DisplayName("Exchange Rate")]
        //public double? ExchangeRate { get; set; }

        public Nullable<long> AccountID { get; set; }
        
        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        public Nullable<int> CostCenterID { get; set; }

    }
}