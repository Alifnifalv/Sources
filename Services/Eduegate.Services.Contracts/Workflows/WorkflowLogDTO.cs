using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Workflows
{
    public class WorkflowLogDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public long? WorkflowTransactionHeadRuleMapID { get; set; }
        public long? HeadID { get; set; }
        public long? WorkflowID { get; set; }
        public long WorkflowRuleID { get; set; }
        public int? ConditionID { get; set; }
        public string ConditionName { get; set; }
        public List<KeyValueDTO> Approvers { get; set; }
        public string Description { get; set; }
        public string StatusDescription { get; set; }
        public int StatusID { get; set; }

        public List<KeyValueDTO> Buttons { get; set; }

        public bool HideButtons { get; set; }

        public bool? IsFlowCompleted { get; set; }
    }
}
