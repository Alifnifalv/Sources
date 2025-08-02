using Newtonsoft.Json;
using Eduegate.Framework;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Workflows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.School.Students;
using Eduegate.Services.Contracts.School.Students;
using System.Data;

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

        public long WorkflowTypeID { get; set; }

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

            vm.WorkflowDescription = wrkDTO.WorkflowDescription;
            vm.WorkflowTypeID = (long)wrkDTO.WorkflowTypeID;
            vm.WorkflowIID = wrkDTO.WorkflowIID;
            vm.WorkflowType = wrkDTO.WorkflowTypeID.HasValue ? new KeyValueViewModel()
            {
                Key = wrkDTO.WorkflowTypeID.ToString(),
                Value = wrkDTO.WorkflowType.Value
            } : new KeyValueViewModel();
            vm.WorkflowField = wrkDTO.WorkflowField.Key != null ? new KeyValueViewModel()
            {
                Key = wrkDTO.WorkflowField.Key.ToString(),
                Value = wrkDTO.WorkflowField.Value
            } : new KeyValueViewModel();

            vm.WorkflowRules = new List<WorkflowRuleViewModel>();

            foreach (var rule in wrkDTO.WorkflowRules)
            {
                var approvalConditions = new List<ApprovalConditionViewModel>();
                var approvers = new List<KeyValueViewModel>();

                foreach (var conditions in rule.ApprovalConditions)
                {
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
                        Condition = conditions.Condition == null ? null : new KeyValueViewModel()
                        {
                            Key = conditions.Condition.Key.ToString(),
                            Value = conditions.Condition.Value
                        },
                        Approvers = approvers,
                    });
                }

                vm.WorkflowRules.Add(new WorkflowRuleViewModel()
                {
                    Condition = rule.Condition == null ? null : new KeyValueViewModel()
                    {
                        Key = rule.Condition.Key.ToString(),
                        Value = rule.Condition.Value
                    },
                    Value1 = rule.Value1,
                    Value2 = rule.Value2,
                    WorkflowRuleIID = rule.WorkflowRuleIID,
                    ApprovalConditions = approvalConditions,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<WorkflowViewModel, WorkflowDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<WorkflowRuleViewModel, WorkflowRulesDTO>.CreateMap();
            Mapper<ApprovalConditionViewModel, ApprovalConditionDTO>.CreateMap();
            return Mapper<WorkflowViewModel, WorkflowDTO>.Map(this);
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
                return null;
            else
                return JsonConvert.DeserializeObject<WorkflowDTO>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto, List<CultureDataInfoDTO> cultures)
        {
            return base.ToVM(dto, cultures);
        }

        public override BaseMasterDTO ToDTO(CallContext context)
        {
            return base.ToDTO(context);
        }
    }
}
