using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
namespace Eduegate.Web.Library.Accounts.AccountTransactions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "GeneralInvoiceTransactionDetails", "CRUDModel.Model.DetailViewModel")]
    public class GeneralInvoiceDetailViewModel : BaseMasterViewModel
    {
        public GeneralInvoiceDetailViewModel()
        {
            AccountSubLedgers = new KeyValueViewModel();
        }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        //[DisplayName("Is Row Selected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }
        public long AccountTransactionDetailIID { get; set; }
        public Nullable<long> AccountTransactionHeadID { get; set; }
        public long? ReceivableID { get; set; }

       

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

       
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width detailview-grid")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("Narration")]
        public string Remarks { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [CustomDisplay("AccountGroup")]
        [Select2("AccountGroup", "Numeric", false, "AccountGroupChanges($event, $element, detail)", false,optionalAttribute1: "ng-disabled='$index === 0'")]
        [LookUp("LookUps.AccountGroup")]
        public KeyValueViewModel AccountGroup { get; set; }
        public long? AccountGroupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width detailview-grid")]
        [CustomDisplay("Account")]
        [Select2("Account", "Numeric", false, "OnGLAccountsCodeChange($select,$index,detail)", false, optionalAttribute1: "ng-disabled='$index === 0' ")]
        [LookUp("LookUps.Account")]
        public KeyValueViewModel Account { get; set; }
        public Nullable<long> AccountID { get; set; }


       

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright", "ng-blur='updateFirstRowDebit(detail)' ng-disabled='detail.Debit > 0 || $index === 0' ")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright", Attributes2 = "ng-disabled= '(detail.Debit > 0) ||( $index === 0)'", Attributes3 = "ng-change='AccountGroupChanges($event, $element, detail)'")]
        [CustomDisplay("Credit")]
        public decimal? Credit { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright", "ng-blur='updateFirstRowDebit(detail)' ng-disabled='detail.Credit > 0 || $index === 0' ")]
        [CustomDisplay("Debit")]
        public decimal? Debit { get; set; }

        public decimal? DebitTotal { get; set; }
        public decimal? CreditTotal { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [Select2("CostCenter", "Numeric", false)]
        [CustomDisplay("CostCenter")]
        [LookUp("LookUps.CostCenterDetails")]
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
       

        [ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width textright", "ng-click='InsertGridRow($index, ModelStructure.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width textleft", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
        public string InvoiceNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public double? InvoiceAmount { get; set; }
        public double? ReturnAmount { get; set; }
        // public string PaymentDueDate { get; set; }
        public double? PaidAmount { get; set; }
        public double? UnpaidAmount { get; set; }
        public decimal? Amount { get; set; }
        public string CurrencyName { get; set; }
        public double? ExchangeRate { get; set; }


    }
}