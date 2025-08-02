using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "RVInvoiceAllocationDetails", "CRUDModel.Model.DetailViewModel")]
    public class RVInvoiceAllocationDetailViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected' ng-click='UnAllocateRowAmount(detail)'")]
        [DisplayName("IsRowSelected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }

        public long ReceivableIID { get; set; }
        public int? DocumentReferenceTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Document")]
        public string DocumentTypeName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Invoice Number")]
        public string InvoiceNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "large-col-width")]
        [DisplayName("Description")]
        public string ReferenceNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("PaymentDueDate")]
        public string PaymentDueDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width textright")]
        [DisplayName("InvoiceAmount")]
        public double? InvoiceAmount { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "small-col-width textright")]
        //[DisplayName("ReturnAmount")]
        //public double? ReturnAmount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width textright")]
        [DisplayName("PaidAmount")]
        public double? PaidAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width textright")]
        [DisplayName("Balance to Pay")]
        public double? BalanceAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright")]
        [DisplayName("Allocate Amount")]
        public double? Amount { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        //[DisplayName("Currency")]
        //[HasClaim(HasClaims.HASMULTICURRENCY)]
        //public string CurrencyName { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        //[DisplayName("ExchangeRate")]
        //[HasClaim(HasClaims.HASMULTICURRENCY)]
        //public double? ExchangeRate { get; set; }

        public Nullable<long> AccountID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        //[DisplayName("Remarks")]
        //public string Remarks { get; set; }

        public Nullable<int> CostCenterID { get; set; }
        public Nullable<long> ReferenceReceiptID { get; set; }
        public Nullable<long> ReferencePaymentID { get; set; }
    }
}
