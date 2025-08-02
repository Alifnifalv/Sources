using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Workflows
{
    [DataContract]
    public class WorkflowRulesDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long WorkflowRuleIID { get; set; }

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
