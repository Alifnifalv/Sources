using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR.Leaves;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class EmployeeJobDescriptionDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public EmployeeJobDescriptionDTO() 
        {
            EmpJobDescriptionDetail = new List<EmployeeJobDescriptionDetailDTO>();
        }

        [DataMember]
        public long JobDescriptionIID { get; set; } 
        
        [DataMember]
        public long? JobApplicationID { get; set; }
        
        [DataMember]
        public string ApplicantName { get; set; }

        [DataMember]
        public bool? IsAgreementSigned { get; set; } 
        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public DateTime? AgreementSignedDate { get; set; }  
        
        [DataMember]
        public string SignedDateString { get; set; } 


        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public long? ReportingToEmployeeID { get; set; }


        [DataMember]
        public string JDReference { get; set; } 
        
        [DataMember]
        public string Department { get; set; } 
        
        [DataMember]
        public string Designation { get; set; }    
        
        [DataMember]
        public string QID { get; set; }  
        
        [DataMember]
        public string RevReference { get; set; }

        [DataMember]
        public string RoleSummary { get; set; }

        [DataMember]
        public string Undertaking { get; set; }
        
        [DataMember]
        public string JobTitle { get; set; }


        [DataMember]
        public DateTime? JDDate { get; set; }  
        
        [DataMember]
        public DateTime? RevDate { get; set; }
   
        [DataMember]
        public KeyValueDTO Employee { get; set; }   
                
        [DataMember]
        public KeyValueDTO ReportingToEmployee { get; set; }

        [DataMember]
        public List<EmployeeJobDescriptionDetailDTO> EmpJobDescriptionDetail { get; set; } 


        public class EmployeeJobDescriptionDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
        {

            [DataMember]
            public long JobDescriptionMapID { get; set; } 

            [DataMember]
            public long? JobDescriptionID { get; set; }

            [DataMember]
            public string Description { get; set; }

        }

    }
   
}
