using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Jobs
{
    public class JobApplicationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public JobApplicationDTO()
        {
            CountryOfResidence = new KeyValueDTO();
        }

        [DataMember]
        public long ApplicationIID { get; set; }
        [DataMember]
        public long? JobID { get; set; }
        [DataMember]
        public long? ApplicantID { get; set; }
        [DataMember]
        public DateTime? AppliedDate { get; set; }
        [DataMember]
        public string ReferenceCode { get; set; }
        [DataMember]
        public KeyValueDTO CountryOfResidence { get; set; }
        [DataMember]
        public int? TotalYearOfExperience { get; set; }
        [DataMember]
        public long? CVContentID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public bool? IsError { get; set; }
        [DataMember]
        public string ReturnMessage { get; set; }

        [DataMember]
        public string ApplicantName { get; set; }
        [DataMember]
        public string AppliedDateString { get; set; } 
        [DataMember]
        public string Education { get; set; }
        [DataMember]
        public string JobTitle { get; set; } 
        [DataMember]
        public string TypeOfJob { get; set; }
        [DataMember]
        public string JobDescription { get; set; }
        [DataMember]
        public string JobDetails { get; set; }        
        [DataMember]
        public string Status { get; set; }        
        [DataMember]
        public string School { get; set; }        
        [DataMember]
        public bool? IsShortListed { get; set; } 
        [DataMember]
        public string ShortListDateString { get; set; }
        [DataMember]
        public string ApplicantMailID { get; set; }
    }
}
