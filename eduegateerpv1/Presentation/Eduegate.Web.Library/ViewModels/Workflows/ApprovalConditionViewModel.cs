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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ApprovalConditions", "gridModel.ApprovalConditions","","","","ruleGrid")]
    [DisplayName("")]
    public class ApprovalConditionViewModel : BaseMasterViewModel
    {
        public ApprovalConditionViewModel()
        {
            Approvers = new List<KeyValueViewModel>();
            Condition = new KeyValueViewModel();
        }

        public long WorkflowRuleConditionIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("Condition", "String", false, onSelectChangeEvent: "OnChangeCondition($select,$index);")]
        [CustomDisplay("Condition")]
        [LookUp("LookUps.ApprovalCondition")]
        public KeyValueViewModel Condition { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("Condition", "String", true, onSelectChangeEvent: "OnChangeApprovers($select,$index);")]
        [CustomDisplay("Approvers")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        public List<KeyValueViewModel> Approvers { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width addremovebutton", "ng-click='RemoveGridRow($index, ModelStructure.WorkflowRules[0], gridModel.ApprovalConditions)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width addremovebutton", "ng-click='InsertGridRow($index, ModelStructure.WorkflowRules[0], gridModel.ApprovalConditions)'")]
        [DisplayName("+")]
        public string Add { get; set; }
    }
}
