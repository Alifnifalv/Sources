using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Workflows;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Workflows
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "WorkflowDetails", "CRUDModel.ViewModel", "class='alignleft two-column-header'")]
    [DisplayName("Workflow Details")]
    public class WorkflowViewModel : BaseMasterViewModel
    {
        public WorkflowViewModel()
        {
            WorkflowType = new KeyValueViewModel();
            WorkflowField = new KeyValueViewModel();
            WorkflowRules = new List<WorkflowRuleViewModel>() { new WorkflowRuleViewModel() };
        }

        public long WorkflowIID { get; set; }

        public int? WorkflowTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("WorkflowType", "String", false)]
        [CustomDisplay("Type")]
        [LookUp("LookUps.WorkflowType")]
        public KeyValueViewModel WorkflowType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "onecol-header-left")]
        [CustomDisplay("Description")]
        public string WorkflowDescription { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("WorkflowField", "String", false)]
        [CustomDisplay("ApplyField")]
        [LookUp("LookUps.WorkflowField")]
        public KeyValueViewModel WorkflowField { get; set; }
        public int? WorkflowApplyFieldID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, "onecol-header-left")]
        [CustomDisplay("Rules")]
        public List<WorkflowRuleViewModel> WorkflowRules { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as WorkflowDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<WorkflowViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<WorkflowDTO, WorkflowViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var wrkDTO = dto as WorkflowDTO;
            var vm = Mapper<WorkflowDTO, WorkflowViewModel>.Map(wrkDTO);

            vm.WorkflowIID = wrkDTO.WorkflowIID;
            vm.WorkflowDescription = wrkDTO.WorkflowDescription;
            vm.WorkflowTypeID = wrkDTO.WorkflowTypeID;
            vm.WorkflowApplyFieldID = wrkDTO.WorkflowApplyFieldID;
            vm.WorkflowType = wrkDTO.WorkflowTypeID.HasValue ? new KeyValueViewModel()
            {
                Key = wrkDTO.WorkflowTypeID.ToString(),
                Value = wrkDTO.WorkflowType.Value
            } : new KeyValueViewModel();
            vm.WorkflowField = wrkDTO.WorkflowApplyFieldID.HasValue ? new KeyValueViewModel()
            {
                Key = wrkDTO.WorkflowField.Key.ToString(),
                Value = wrkDTO.WorkflowField.Value
            } : new KeyValueViewModel();

            vm.WorkflowRules = new List<WorkflowRuleViewModel>();
            foreach (var rule in wrkDTO.WorkflowRules)
            {
                var approvalConditions = new List<ApprovalConditionViewModel>();
                foreach (var conditions in rule.ApprovalConditions)
                {
                    var approvers = new List<KeyValueViewModel>();
                    foreach (var approver in conditions.Approvers)
                    {
                        approvers.Add(new KeyValueViewModel()
                        {
                            Key = approver.Key.ToString(),
                            Value = approver.Value.ToString()
                        });
                    }

                    approvalConditions.Add(new ApprovalConditionViewModel()
                    {
                        WorkflowRuleConditionIID = conditions.WorkflowRuleConditionIID,
                        Condition = conditions.ConditionID.HasValue ? new KeyValueViewModel()
                        {
                            Key = conditions.Condition.Key.ToString(),
                            Value = conditions.Condition.Value
                        } : new KeyValueViewModel(),
                        Approvers = approvers,
                    });
                }

                vm.WorkflowRules.Add(new WorkflowRuleViewModel()
                {
                    WorkflowRuleIID = rule.WorkflowRuleIID,
                    Condition = rule.ConditionID.HasValue ? new KeyValueViewModel()
                    {
                        Key = rule.Condition?.Key,
                        Value = rule.Condition?.Value
                    } : new KeyValueViewModel(),
                    Value1 = rule.Value1,
                    Value2 = rule.Value2,
                    ApprovalConditions = approvalConditions,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<WorkflowViewModel, WorkflowDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<WorkflowViewModel, WorkflowDTO>.Map(this);

            dto.WorkflowIID = this.WorkflowIID;
            dto.WorkflowDescription = this.WorkflowDescription;
            dto.WorkflowTypeID = string.IsNullOrEmpty(this.WorkflowType.Key) ? (int?)null : int.Parse(this.WorkflowType.Key);
            dto.WorkflowApplyFieldID = string.IsNullOrEmpty(this.WorkflowField.Key) ? (int?)null : int.Parse(this.WorkflowField.Key);

            dto.WorkflowRules = new List<WorkflowRulesDTO>();
            foreach (var rule in this.WorkflowRules)
            {
                var conditions = new List<ApprovalConditionDTO>();
                foreach (var condition in rule.ApprovalConditions)
                {
                    var approvers = new List<KeyValueDTO>();
                    if (!string.IsNullOrEmpty(condition.Condition.Key))
                    {
                        foreach (var approver in condition.Approvers)
                        {
                            if (!string.IsNullOrEmpty(approver.Key))
                            {
                                approvers.Add(new KeyValueDTO()
                                {
                                    Key = approver.Key,
                                    Value = approver.Value,
                                });
                            }
                        }
                        conditions.Add(new ApprovalConditionDTO()
                        {
                            WorkflowRuleConditionIID = condition.WorkflowRuleConditionIID,
                            WorkflowRuleID = condition.WorkflowRuleID,
                            ConditionID = string.IsNullOrEmpty(condition.Condition.Key) ? (int?)null : int.Parse(condition.Condition.Key),
                            Approvers = approvers,
                        });
                    }
                }

                if (!string.IsNullOrEmpty(rule.Value1))
                {
                    dto.WorkflowRules.Add(new WorkflowRulesDTO()
                    {
                        WorkflowRuleIID = rule.WorkflowRuleIID,
                        WorkflowID = rule.WorkflowID,
                        ConditionID = string.IsNullOrEmpty(rule.Condition.Key) ? (int?)null : int.Parse(rule.Condition.Key),
                        Value1 = rule.Value1,
                        Value2 = rule.Value2,
                        ApprovalConditions = conditions,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<WorkflowDTO>(jsonString);
        }

    }
}
