using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Details", "CRUDModel.ViewModel.Details")]
    [DisplayName("")]
    public class ChartOfAccountDetailViewModel : BaseMasterViewModel
    {
        public ChartOfAccountDetailViewModel()
        {
            IsGLAccount = false;
            GLAccount = new KeyValueViewModel();
        }

        public long ChartOfAccountMapIID { get; set; }
        public long? AccountID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [DisplayName("Account Name")]
        [Select2("Account", "Numeric", false, "OnGLAccountChangeSelect2($select,$index, gridModel)", true)]
        [LookUp("LookUps.Account")]
        public KeyValueViewModel GLAccount { get; set; }

        public bool IsGLAccount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "", "", "ng-disabled=gridModel.IsGLAccount")]
        [DisplayName("Code")]
        public string AccountCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown , "large-col-width")]
        [DisplayName("Income/Balance")]
        [LookUp("LookUps.IncomeOrBalance")]
        public string IncomeOrBalance { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, "large-col-width", "", "", "ng-disabled=gridModel.IsGLAccount")]
        [DisplayName("Type")]
        [LookUp("LookUps.ChartRowType")]
        public string ChartRowType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Total")]
        public decimal Total { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Net Change")]
        public decimal NetChange { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Balance")]
        public decimal Balance { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='InsertGridRow($index, ModelStructure.Details[0], CRUDModel.ViewModel.Details)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='RemoveGridRow($index, CRUDModel.SchedulerInfo.Schedulers[0], CRUDModel.ViewModel.SchedulerInfo.Schedulers)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        public int ChartRowTypeID { get; set; }
        public int NoOfBlankLines { get; set; }
        public bool IsNewPage { get; set; }
        public long AccountGroupID { get; set; }
        public string AccountGroupName { get; set; }
    }
}
