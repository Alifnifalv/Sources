using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "RVInvoiceTransactionDetails", "CRUDModel.Model.DetailViewModel")]
    public class RVInvoiceDetailViewModel : BaseMasterViewModel
    {
        public RVInvoiceDetailViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        [DisplayName("Is Selected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }
        public long AccountTransactionDetailIID { get; set; }
        public Nullable<long> AccountTransactionHeadID { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public long? ReceivableID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("InvoiceNumber")]
        public string InvoiceNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "large-col-width")]
        [CustomDisplay("Ref.No")]
        public string ReferenceNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("PaymentDueDate")]
        public string PaymentDueDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width textright")]
        [CustomDisplay("InvoiceAmount")]
        public double? InvoiceAmount { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "small-col-width textright")]
        //[DisplayName("ReturnAmount")]
        //public double? ReturnAmount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width textright")]
        [CustomDisplay("PaidAmount")]
        public double? PaidAmount { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "small-col-width textright")]
        //[DisplayName("UnPaid Amount")]
        //public double? UnPaidAmount { get; set; }


        //[Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be numeric")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright",  attribs: "ng-blur=BalanceAmountChanges(detail)")]
        [CustomDisplay("BalanceToPay")]
        public double? Amount { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        //[DisplayName("Currency")]
        //[HasClaim(HasClaims.HASMULTICURRENCY)]
        //public string CurrencyName { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        //[DisplayName("Exchange Rate")]
        //[HasClaim(HasClaims.HASMULTICURRENCY)]
        //public double? ExchangeRate { get; set; }
        
        public Nullable<long> AccountID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        public Nullable<int> CostCenterID { get; set; }
        public Nullable<long> ReferenceReceiptID { get; set; }
        public Nullable<long> ReferencePaymentID { get; set; }
    }
}