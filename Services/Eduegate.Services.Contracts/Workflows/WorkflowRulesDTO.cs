using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Workflows
{
    [DataContract]
    public class WorkflowRulesDTO : BaseMasterDTO
    {
        public WorkflowRulesDTO()
        {
            Condition = new KeyValueDTO();
            ApprovalConditions = new List<ApprovalConditionDTO>();
        }

        [DataMember]
        public long WorkflowRuleIID { get; set; }

        [DataMember]
        public long? WorkflowID { get; set; }

        [DataMember]
        public int? ConditionID { get; set; }

        [DataMember]
        public KeyValueDTO Condition { get; set; }

        [DataMember]
        public string Value1 { get; set; }

        [DataMember]
        public string Value2 { get; set; }

        [DataMember]
        public string Value3 { get; set; }

        [DataMember]
        public List<ApprovalConditionDTO> ApprovalConditions { get; set; }
    }
}
