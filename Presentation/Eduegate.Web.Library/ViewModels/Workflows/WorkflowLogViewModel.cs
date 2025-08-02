using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Workflows;
using System;
using System.Collections.Generic;

namespace Eduegate.Web.Library.ViewModels.Workflows
{
    public class WorkflowLogViewModel
    {
        public WorkflowLogViewModel()
        {
            Buttons = new List<KeyValueViewModel>();
            Approvers = new List<KeyValueViewModel>();
        }

        public long? WorkflowTransactionHeadRuleMapID { get; set; }

        public long? HeadID { get; set; }

        public long? WorkflowID { get; set; }

        public long WorkflowRuleID { get; set; }

        public int? ConditionID { get; set;}

        public string ConditionName { get; set; }

        public List<KeyValueViewModel> Approvers { get; set; }

        public string Description { get; set; }

        public string StatusDescription { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int StatusID { get; set; }

        public long? LoggedinLoginID { get; set; }

        public long? LoggedinEmployeeID { get; set; }

        public List<KeyValueViewModel> Buttons { get; set; }

        public bool HideButtons { get; set; }

        public bool? IsFlowCompleted { get; set; }

        public static List<WorkflowLogViewModel> FromDTO(List<WorkflowLogDTO> dtos)
        {
            var vms = new List<WorkflowLogViewModel>();
            foreach(var dto in dtos)
            {
                vms.Add(FromDTO(dto));
            }
            
            return vms;
        }

        public static WorkflowLogViewModel FromDTO(WorkflowLogDTO dto)
        {
            Mapper<WorkflowLogDTO, WorkflowLogViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();   
            return Mapper<WorkflowLogDTO, WorkflowLogViewModel>.Map(dto);
        }

        public static WorkflowLogDTO ToVM(WorkflowLogViewModel vm)
        {
            Mapper<WorkflowLogViewModel, WorkflowLogDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            return Mapper<WorkflowLogViewModel, WorkflowLogDTO>.Map(vm);
        }

    }
}