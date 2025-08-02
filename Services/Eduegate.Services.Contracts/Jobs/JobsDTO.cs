using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Jobs
{ 
    public class JobsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public JobsDTO()
        {
            JobApplicationDTO = new List<JobApplicationDTO>();
            AvailableJobCriteriaMapDTO = new List<AvailableJobCriteriaMapDTO>();
            SkillList = new List<KeyValueDTO>();
            Country = new KeyValueDTO();
            JDReference = new KeyValueDTO();
        }

        [DataMember]
        public long JobIID { get; set; }
        [DataMember]
        public string JobTitle { get; set; }
        [DataMember]
        public string TypeOfJob { get; set; }
        [DataMember]
        public string JobDescription { get; set; }
        [DataMember]
        public string JobDetails { get; set; } 
        [DataMember]
        public string MonthlySalary { get; set; } 
        [DataMember]
        public int? TotalYearOfExperience { get; set; }
        [DataMember]
        public int? NoOfVacancies { get; set; }
        [DataMember]
        public string Status { get; set; } 
        [DataMember]
        public byte? JobStatusID { get; set; } 
        [DataMember]
        public int? JobTypeID { get; set; }    
        [DataMember]
        public long? DepartmentID { get; set; } 
        [DataMember]
        public int? DesignationID { get; set; } 
        
        [DataMember]
        public string School { get; set; }   
        
        [DataMember]
        public string PostedDate { get; set; }

        [DataMember]
        public string CloseDate { get; set; }  

        [DataMember]
        public string AppliedDate { get; set; } 
        
        [DataMember]
        public bool?  IsJobDetailPage { get; set; }
        
        [DataMember]
        public bool?  IsJobApplyPage { get; set; }
        [DataMember]
        public string JobTitleInitial { get; set; }
        [DataMember]
        public int? CountryID { get; set; } 
        [DataMember]
        public long? JDReferenceID  { get; set; } 

        [DataMember]
        public KeyValueDTO Country { get; set; }   
        
        [DataMember]
        public KeyValueDTO JDReference { get; set; }        

        [DataMember]
        public DateTime? PublishDate { get; set; }
        [DataMember]
        public DateTime? ClosingDate { get; set; }

        [DataMember]
        public bool? IsJobAlreadyApplied { get; set; }
        [DataMember]
        public string Location { get; set; }

        //For Applicant ShortList Screen
        [DataMember]
        public List<JobApplicationDTO> JobApplicationDTO { get; set; }

        [DataMember]
        public List<KeyValueDTO> SkillList { get; set; }
        
        
        [DataMember]
        public List<AvailableJobCriteriaMapDTO> AvailableJobCriteriaMapDTO { get; set; }
    }
}
