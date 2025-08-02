using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class ClassSubjectWorkFlowMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public ClassSubjectWorkFlowMapDTO()
        {

        }

        [DataMember]
        public long ClassSubjectWorkflowEntityMapIID { get; set; }

        [DataMember]
        public long ClassSubjectMapID { get; set; }

        [DataMember]
        public int? WorkflowEntityID { get; set; }

        [DataMember]
        public long? workflowID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public string WorkFlowEntity { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string WorkFlow { get; set; }
    }
}


