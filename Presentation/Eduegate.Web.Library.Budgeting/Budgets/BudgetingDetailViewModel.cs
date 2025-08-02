using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eduegate.Web.Library.Budgeting.Budget
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "BudgetingDetail", "CRUDModel.ViewModel.BudgetingDetail")]
    [DisplayName("Budgeting Detail")]
    public class BudgetingDetailViewModel : BaseMasterViewModel
    {
        public BudgetingDetailViewModel()
        {
            BudgetingAccountDetail = new List<BudgetingAccountDetailViewModel>() { new BudgetingAccountDetailViewModel() };
        }

        public long? BudgetingIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "medium - col - width textleft")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; } = false;

        public long? AccountGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Account Group")]
        public string AccountGroup { get; set; }


        public List<string> HeaderTitles { get; set; }


        [ControlType(Framework.Enums.ControlTypes.GridGroup, "BudgetingAccountDetail", Attributes2 = "colspan=14")]
        [CustomDisplay("")]
        public List<BudgetingAccountDetailViewModel> BudgetingAccountDetail { get; set; }

    }
}