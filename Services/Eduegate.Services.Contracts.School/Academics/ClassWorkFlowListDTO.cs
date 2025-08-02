using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class ClassWorkFlowListDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public ClassWorkFlowListDTO()
        {

        }
        [DataMember]
        public long ClassWorkFlowIID { get; set; }

        [DataMember]
        public int?  ClassID { get; set; }

        [DataMember]
        public int? WorkflowEntityID { get; set; }

        [DataMember]
        public long? WorkflowID { get; set; }

    }
}



