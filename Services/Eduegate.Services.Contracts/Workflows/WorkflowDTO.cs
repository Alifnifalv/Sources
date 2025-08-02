using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Workflows
{
    [DataContract]
    public class WorkflowDTO : BaseMasterDTO
    {
        public WorkflowDTO()
        {
            WorkflowType = new KeyValueDTO();
            WorkflowField = new KeyValueDTO();
            WorkflowRules = new List<WorkflowRulesDTO>();
        }

        [DataMember]
        public long WorkflowIID { get; set; }

        [DataMember]
        public int? WorkflowTypeID { get; set; }

        [DataMember]
        public int? LinkedEntityTypeID { get; set; }

        [DataMember]
        public int? WorkflowApplyFieldID { get; set; }

        [DataMember]
        public KeyValueDTO WorkflowType { get; set; }

        [DataMember]
        public KeyValueDTO WorkflowField { get; set; }

        [DataMember]
        public string WorkflowDescription { get; set; }

        [DataMember]
        public List<WorkflowRulesDTO> WorkflowRules { get; set; }

        #region For Worflow Mail
        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public long? WorkflowStatusID { get; set; }

        [DataMember]
        public long? WorkflowReferenceID { get; set; }

        [DataMember]
        public bool WorkflowResult { get; set; }
        #endregion

    }
}