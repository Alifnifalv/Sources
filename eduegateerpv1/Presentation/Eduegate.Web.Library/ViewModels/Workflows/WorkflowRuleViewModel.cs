using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Workflows
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "workflowRules", "CRUDModel.ViewModel.WorkflowRules")]
    [DisplayName("")]
    public class WorkflowRuleViewModel : BaseMasterViewModel
    {
        public WorkflowRuleViewModel()
        {
            Condition = new KeyValueViewModel();
            ApprovalConditions = new List<ApprovalConditionViewModel>() { new ApprovalConditionViewModel() };
        }

        public long WorkflowRuleIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("Condition", "String", false, onSelectChangeEvent: "OnChangeCondition($select,$index);")]
        [CustomDisplay("Condition")]
        [LookUp("LookUps.WorkflowCondition")]
        public KeyValueViewModel Condition { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width", "", "", ""/*"ng-disabled='CRUDModel.ViewModel.HasReferenceID' ng-init='gridModel.ReferenceID=CRUDModel.ViewModel.ReferenceID'"*/, false)]
        [CustomDisplay("Value")]
        public string Value1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width", "", "", /*"ng-disabled='CRUDModel.ViewModel.HasReferenceID' ng-init='gridModel.ReferenceID=CRUDModel.ViewModel.ReferenceID'"*/ "", false)]
        [CustomDisplay("Value2")]
        public string Value2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, "onecol-header-left")]
        [CustomDisplay("ApprovalFlow")]
        public List<ApprovalConditionViewModel> ApprovalConditions { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width addremovebutton", "ng-click='RemoveGridRow($index, ModelStructure.WorkflowRules[0], CRUDModel.ViewModel.WorkflowRules)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width addremovebutton", "ng-click='InsertGridRow($index, ModelStructure.WorkflowRules[0], CRUDModel.ViewModel.WorkflowRules)'")]
        [DisplayName("+")]
        public string Add { get; set; }
    }
}
