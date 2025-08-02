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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ExpenditureAllocTransactionAlloc", "gridModel.ExpenditureAllocTransactionAlloc")]
    [DisplayName("Transactions")]
    public class ExpenditureAllocationTransactionsAllocViewModel : BaseMasterViewModel
    {
        public long AccountTransactionDetailIID { get; set; }
        public Nullable<long> AccountTransactionHeadID { get; set; }



        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [DisplayName("Type of expense")]
        [Select2("ExpenseLedgerAccounts", "Numeric", false, "")]
        [LookUp("LookUps.ExpenseLedgerAccounts")]
        public KeyValueViewModel Account { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "large-col-width")]
        [DisplayName("Narration")]
        public string Remarks { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown, "medium-col-width", attribs: "ng-change=TaxTemplateChange($event,detail)")]
        //[LookUp("LookUps.TaxTemplates")]
        //[DisplayName("Tax")]
        //[HasClaim(HasClaims.HASVAT)]
        //public string TaxTemplate { get; set; }

        //public int? TaxTemplateID { get; set; }
        //public decimal? TaxPercentage { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright", Attributes = "ng-disabled='(gridModel.DebitAmount > 0)'" ,Attributes2 =  "ng-blur=checkSumAmount(CRUDModel.ViewModel.ExpenditureAllocTransactions[$parent.$index],$index)")]
        [CustomDisplay("Debit")]
        public decimal? DebitAmount { get; set; } = 0;

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright",   Attributes = "ng-disabled='(gridModel.DebitAmount > 0)'",Attributes2 = "ng-blur=checkSumAmount(CRUDModel.ViewModel.ExpenditureAllocTransactions[$parent.$index],$index)")]
        [CustomDisplay("Credit")]
        public decimal? CreditAmount { get; set; } = 0;

        //[ControlType(Framework.Enums.ControlTypes.HiddenWithLabel, "medium-col-width textright", "{{detail.TaxAmount=detail.DebitAmount*(detail.TaxPercentage/100);detail.InclusiveTaxAmount=(!detail.HasTaxInclusive? 0 : detail.DebitAmount*(detail.TaxPercentage/100));detail.ExclusiveTaxAmount=(detail.HasTaxInclusive? 0 : detail.DebitAmount*(detail.TaxPercentage/100))}}")]
        //[DisplayName("Tax Amount")]
        //[HasClaim(HasClaims.HASVAT)]
        //public double TaxAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.gridModel.ExpenditureAllocTransactionAlloc[0], gridModel.ExpenditureAllocTransactionAlloc)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.gridModel.ExpenditureAllocTransactionAlloc[0],gridModel.ExpenditureAllocTransactionAlloc)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        public Nullable<int> CostCenterID { get; set; }

        public string InvoiceNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public double? InvoiceAmount { get; set; }
        public double? ReturnAmount { get; set; }
        // public string PaymentDueDate { get; set; }
        public double? PaidAmount { get; set; }
        public double? UnpaidAmount { get; set; }
        public decimal? Amount { get; set; }
        public string CurrencyName { get; set; }
        public decimal ExchangeRate { get; set; }
        public Nullable<long> AccountID { get; set; }
        //public string Remarks { get; set; }
        public KeyValueViewModel CostCenter { get; set; }
        public decimal? InclusiveTaxAmount { get; set; }
        public decimal? ExclusiveTaxAmount { get; set; }
        public bool? HasTaxInclusive { get; set; }
    }
}
