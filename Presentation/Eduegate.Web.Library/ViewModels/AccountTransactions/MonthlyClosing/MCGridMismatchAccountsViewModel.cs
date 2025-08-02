using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions.MonthlyClosing
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "MCGridMismatchAccounts", "CRUDModel.ViewModel.MCTabMisMatchedAccounts.MCGridMismatchAccounts")]
    [DisplayName("Accounts")]
    public class MCGridMismatchAccountsViewModel : BaseMasterViewModel
    {  
        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Tran Type Name")]
        public string TranTypeName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Document Type Name")]
        public string DocumentTypeName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Branch")]
        public string Branch { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Tran Date")]
        public string TranDateString { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Tran No.")]
        public string TranNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Voucher No.")]
        public string VoucherNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Narration")]
        public string Narration { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("Amount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }
    }
}
