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
    public class WorkflowDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long WorkflowIID { get; set; }

        [DataMember]
        public int? WorkflowTypeID { get; set; }

        [DataMember]
        public KeyValueDTO WorkflowType { get; set; }

        [DataMember]
        public KeyValueDTO WorkflowField { get; set; }

        [DataMember]
        public string WorkflowDescription { get; set; }

        [DataMember]
        public List<WorkflowRulesDTO> WorkflowRules { get; set; }


        //For Worflow Mail
        
        [DataMember]
        public long? EmployeeID { get; set; }  
        
        [DataMember]
        public long? WorkflowStatusID { get; set; }
        
        [DataMember]
        public long? WorkflowReferenceID { get; set; } 
        
        [DataMember]
        public bool WorkflowResult { get; set; }
    }
}
