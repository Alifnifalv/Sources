using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Workflows
{
    [DataContract]
    public class ApprovalConditionDTO : BaseMasterDTO
    {
        public ApprovalConditionDTO()
        {
            Condition = new KeyValueDTO();
            Approvers = new List<KeyValueDTO>();
        }

        [DataMember]
        public long WorkflowRuleConditionIID { get; set; }

        [DataMember]
        public long? WorkflowRuleID { get; set; }

        [DataMember]
        public int? ConditionID { get; set; }

        [DataMember]
        public KeyValueDTO Condition { get; set; }

        [DataMember]
        public List<KeyValueDTO> Approvers { get; set; }
    }
}