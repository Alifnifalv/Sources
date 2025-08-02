using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.DetailViewModel")]
    public class JournalDetailViewModel : BaseMasterViewModel
    {
        public JournalDetailViewModel()
        {
            AccountSubLedgers = new KeyValueViewModel();
            Budget = new KeyValueViewModel();
        }

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
        [CustomDisplay("AccountGroup")]
        [Select2("AccountGroup", "Numeric", false, "AccountGroupChanges($event, $element, detail)", false)]
        [LookUp("LookUps.AccountGroup")]
        public KeyValueViewModel AccountGroup { get; set; }
        public long? AccountGroupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width detailview-grid")]
        [CustomDisplay("Account")]
        [Select2("Account", "Numeric", false, "OnGLAccountsCodeChange($select,$index,detail)")]
        [LookUp("LookUps.Account")]
        public KeyValueViewModel Account { get; set; }
        public Nullable<long> AccountID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [CustomDisplay("Narration")]
        public string Narration { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright", "ng-disabled = '(detail.Credit > 0)'")]
        [CustomDisplay("Debit")]
        public decimal? Debit { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright", "ng-disabled = '(detail.Debit > 0)'")]
        [CustomDisplay("Credit")]
        public decimal? Credit { get; set; }

        public decimal? Amount { get; set; }

        public decimal? DebitTotal { get; set; }
        public decimal? CreditTotal { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [Select2("CostCenter", "Numeric", false)]
        [CustomDisplay("CostCenter")]
        [LookUp("LookUps.CostCenter")]
        public KeyValueViewModel CostCenter { get; set; }
        public Nullable<int> CostCenterID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [Select2("Budget", "Numeric", false)]
        [CustomDisplay("Budget")]
        [LookUp("LookUps.Budget")]
        public KeyValueViewModel Budget { get; set; }
        public Nullable<int> BudgetID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [Select2("AccountSubLedgers", "Numeric", false, "")]
        [CustomDisplay("SubLedger")]
        [LookUp("LookUps.AccountSubLedgers")]
        public KeyValueViewModel AccountSubLedgers { get; set; }
        public long? SubLedgerID { get; set; }

     
        public string InvoiceNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public double? InvoiceAmount { get; set; }
        public double? ReturnAmount { get; set; }
        // public string PaymentDueDate { get; set; }
        public double? PaidAmount { get; set; }
        public double? UnpaidAmount { get; set; }
        public string CurrencyName { get; set; }
        public double? ExchangeRate { get; set; }
        public string Remarks { get; set; }
        //public KeyValueViewModel CostCenter { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width textright", "ng-click='InsertGridRow($index, ModelStructure.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width textleft", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
