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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "JVDetails", "CRUDModel.Model.DetailViewModel")]
    public class JVDetailViewModel : BaseMasterViewModel
    {
        public JVDetailViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        [DisplayName("IsRowSelected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }
        public long AccountTransactionDetailIID { get; set; }
        public Nullable<long> AccountTransactionHeadID { get; set; }
        public long? ReceivableID { get; set; }



        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [Select2("AccountTypes", "Numeric", false)]
        [DisplayName("AccountTypes")]
        [LookUp("LookUps.AccountTypes")]
        public KeyValueViewModel AccountTypes { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]

        [DisplayName("ChartOfAccount")]
        [Select2("Accounts", "Numeric", false, "", false, optionalAttribute1:"ng-hide=detail.AccountTypes.Key!=1")]
        [LazyLoad("", "AssetMaster/AccountCodesSearch", "LookUps.Accounts")]
        public KeyValueViewModel Account { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [DisplayName("VendorCustomerAccounts")]
        [Select2("VendorCustomerAccounts", "Numeric", false, "", false, optionalAttribute1: "ng-hide=detail.AccountTypes.Key!=2")]
        [LazyLoad("", "Accounts/PVRegularPayment/GetVendorCustomerAccountsSearch", "LookUps.VendorCustomerAccounts")]
        public KeyValueViewModel VendorCustomerAccounts { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright", "ng-disabled='(detail.Debit > 0)  '")]
        [DisplayName("Credit")]
        public double? Credit { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright", "ng-disabled = '(detail.Credit > 0)'")]
        [DisplayName("Debit")]
        public double? Debit { get; set; }

        public double? DebitTotal { get; set; }
        public double? CreditTotal { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [Select2("CostCenter", "Numeric", false)]
        [DisplayName("CostCenter")]
        [LookUp("LookUps.CostCenter")]
        public KeyValueViewModel CostCenter { get; set; }

        public Nullable<int> CostCenterID { get; set; }

        public string InvoiceNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public double? InvoiceAmount { get; set; }
        public double? ReturnAmount { get; set; }
       // public string PaymentDueDate { get; set; }
        public double? PaidAmount { get; set; }
        public double? UnpaidAmount { get; set; }
        public double? Amount { get; set; }
        public string CurrencyName { get; set; }
        public decimal ExchangeRate { get; set; }
        public Nullable<long> AccountID { get; set; }
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.DetailViewModel, CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }



    }
}


