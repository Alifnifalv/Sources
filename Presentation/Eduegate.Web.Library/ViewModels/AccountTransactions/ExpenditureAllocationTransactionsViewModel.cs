using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ExpenditureAllocTransactions", "CRUDModel.ViewModel.ExpenditureAllocTransactions")]
    [DisplayName("Transactions")]
    public class ExpenditureAllocationTransactionsViewModel : BaseMasterViewModel
    {
        public ExpenditureAllocationTransactionsViewModel()
        {
            //VendorCustomerAccounts = new KeyValueViewModel();
            //AccountTypes = new KeyValueViewModel();
            Account = new KeyValueViewModel();
            CostCenter = new KeyValueViewModel();
            AccountGroup = new KeyValueViewModel();
            AccountSubLedgers = new KeyValueViewModel();
            ExpenditureAllocTransactionAlloc = new List<ExpenditureAllocationTransactionsAllocViewModel>() { new ExpenditureAllocationTransactionsAllocViewModel() };
        }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        //[DisplayName("IsRowSelected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }
        public long AccountTransactionDetailIID { get; set; }
        public Nullable<long> AccountTransactionHeadID { get; set; }
        public long? ReceivableID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        //[Select2("AccountTypes", "Numeric", false)]
        //[DisplayName("Account types")]
        //[LookUp("LookUps.AccountTypes")]
        //public KeyValueViewModel AccountTypes { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        ////[DisplayName("Chart of a/c")]
        //[Select2("Accounts", "Numeric", false, "", false, optionalAttribute1: "ng-hide=detail.AccountTypes.Key!=1")]
        //[LazyLoad("", "AssetMaster/AccountCodesSearch", "LookUps.Accounts")]
        //public KeyValueViewModel Account { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        //[DisplayName("Vendor customer a/c")]
        //[Select2("VendorCustomerAccounts", "Numeric", false, "", false, optionalAttribute1: "ng-hide=detail.AccountTypes.Key!=2")]
        //[LazyLoad("", "Accounts/Accounts/PVRegularPayment/GetVendorCustomerAccountsSearch", "LookUps.VendorCustomerAccounts")]
        //public KeyValueViewModel VendorCustomerAccounts { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [CustomDisplay("AccountGroup")]
        [Select2("AccountGroup", "Numeric", false, optionalAttribute1: "ng-disabled=true")]
        [LookUp("LookUps.AccountGroup")]
        public KeyValueViewModel AccountGroup { get; set; }
        public long? AccountGroupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [CustomDisplay("Account")]
        [Select2("Account", "Numeric", false, optionalAttribute1: "ng-disabled=true")]
        [LookUp("LookUps.Account")]
        public KeyValueViewModel Account { get; set; }
        public Nullable<long> AccountID { get; set; }

        //[Required]Attributes = "ng-disabled=
        [ControlType(Framework.Enums.ControlTypes.TextBox, "large-col-width textleft", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Description")]
        public string Remarks { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Debit")]
        public decimal? Debit { get; set; } = 0;


        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Credit")]
        public decimal? Credit { get; set; } = 0;

        public decimal? DebitTotal { get; set; }
        public decimal? CreditTotal { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "ExpenditureAllocTransactionAlloc")]//, Attributes4 = "colspan=4")]
        [DisplayName("")]
        public List<ExpenditureAllocationTransactionsAllocViewModel> ExpenditureAllocTransactionAlloc { get; set; }


        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        //[Select2("CostCenter", "Numeric", false)]
        //[CustomDisplay("CostCenter")]
        //[LookUp("LookUps.CostCenter")]
        public KeyValueViewModel CostCenter { get; set; }
        public Nullable<int> CostCenterID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        //[Select2("Budget", "Numeric", false)]
        //[CustomDisplay("Budget")]
        //[LookUp("LookUps.Budget")]
        public KeyValueViewModel Budget { get; set; }
        public Nullable<int> BudgetID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        //[Select2("AccountSubLedgers", "Numeric", false, "")]
        //[CustomDisplay("SubLedger")]
        //[LookUp("LookUps.AccountSubLedgers")]
        public KeyValueViewModel AccountSubLedgers { get; set; }
        public long? SubLedgerID { get; set; }

        public string InvoiceNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public double? InvoiceAmount { get; set; }
        public double? ReturnAmount { get; set; }

        public double? PaidAmount { get; set; }
        public double? UnpaidAmount { get; set; }
        public decimal? Amount { get; set; }
        public string CurrencyName { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
