using Eduegate.Framework.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions.MonthlyClosing
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabMisMatchedAccounts", "CRUDModel.ViewModel.MCTabMisMatchedAccounts")]
    [DisplayName("Mismatched Entries of Accounts & Inventories(Amount,Ledger,Product,Cost,Invoice Amount,Quantity,Inner Companies etc)")]
    public  class MCTabMisMatchedAccountsInventoryViewModel : BaseMasterViewModel
    {
        public MCTabMisMatchedAccountsInventoryViewModel()
        {
            MCGridMismatchAccounts = new List<MCGridMismatchAccountsViewModel>() { new MCGridMismatchAccountsViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Mismatched Entries of Accounts & Inventories")]
        public List<MCGridMismatchAccountsViewModel> MCGridMismatchAccounts { get; set; }
    }
}
