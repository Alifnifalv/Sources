using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR.Leaves;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class JobDescriptionDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public JobDescriptionDTO() 
        {
            JDDetail = new List<JobDescriptionDetailDTO>();
        }

        [DataMember]
        public long JDMasterIID { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public long? ReportingToEmployeeID { get; set; }


        [DataMember]
        public string JDReference { get; set; } 
        
        
        [DataMember]
        public string RevReference { get; set; }

        [DataMember]
        public string RoleSummary { get; set; }

        [DataMember]
        public string Undertaking { get; set; } 
        
        [DataMember]
        public string Responsibilities { get; set; }


        [DataMember]
        public DateTime? JDDate { get; set; }  
        
        [DataMember]
        public DateTime? RevDate { get; set; }

        [DataMember]
        public long? DepartmentID { get; set; }

        [DataMember]
        public int? DesignationID { get; set; }


        [DataMember]
        public KeyValueDTO ReportingToEmployee { get; set; }

        [DataMember]
        public List<JobDescriptionDetailDTO> JDDetail { get; set; } 


        public class JobDescriptionDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
        {

            [DataMember]
            public long JDMapID { get; set; } 

            [DataMember]
            public long? JDMasterID { get; set; } 

            [DataMember]
            public string Description { get; set; }

        }

    }
   
}
