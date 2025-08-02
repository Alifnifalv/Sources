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
    public class PVInvoiceAllocationDetailViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        [DisplayName("IsRowSelected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("InvoiceNumber")]
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

        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be numeric")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright")]
        [DisplayName("Balance to Pay")]
        public double? Amount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Currency")]
        [HasClaim(HasClaims.HASMULTICURRENCY)]
        public string CurrencyName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("ExchangeRate")]
        [HasClaim(HasClaims.HASMULTICURRENCY)]
        public double? ExchangeRate { get; set; }

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
