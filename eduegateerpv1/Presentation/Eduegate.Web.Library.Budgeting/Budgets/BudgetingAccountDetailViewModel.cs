using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eduegate.Web.Library.Budgeting.Budget
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "BudgetingAccountDetail", "gridModel.BudgetingAccountDetail")]
    [DisplayName("Budgeting Account Detail")]
    public class BudgetingAccountDetailViewModel : BaseMasterViewModel
    {
        public BudgetingAccountDetailViewModel()
        {
            AccountGroup = new KeyValueViewModel();
            Account = new KeyValueViewModel();
            Budget = new KeyValueViewModel();
            AccountListModel = new List<BudgetingAccountDetailViewModel>();
        }

        public long BudgetEntryAccountMapIID { get; set; }

        public long? BudgetEntryID { get; set; }
        public KeyValueViewModel Budget { get; set; }

        public KeyValueViewModel AccountGroup { get; set; }
        public int? AccountGroupID { get; set; }

        public KeyValueViewModel Account { get; set; }
        public long? AccountID { get; set; }

        public int? CostCenterID { get; set; }

        public string Remarks { get; set; }

        public string GroupDefaultSide { get; set; }

        public List<BudgetingAccountDetailViewModel> AccountListModel { get; set; }

    }
}