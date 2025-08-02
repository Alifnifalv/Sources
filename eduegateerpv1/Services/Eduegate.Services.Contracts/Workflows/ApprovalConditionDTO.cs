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
    public class ApprovalConditionDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long WorkflowRuleConditionIID { get; set; }
        [DataMember]
        public KeyValueDTO Condition { get; set; }
        [DataMember]
        public List<KeyValueDTO> Approvers { get; set; }
    }
}
