using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Jobs
{
    public class JobInterviewDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public JobInterviewDTO()
        {
            ShortListDTO = new List<JobInterviewMapDTO>();
            InterviewRounds = new List<KeyValueDTO>();
        }

        [DataMember]
        public long InterviewID { get; set; } 
        [DataMember]
        public string Interview { get; set; }

        [DataMember]
        public long? JobID { get; set; }
        [DataMember]
        public DateTime? StartDate { get; set; }
        [DataMember]
        public DateTime? EndDate { get; set; }
        [DataMember]
        public long? InterviewerID { get; set; }
        [DataMember]
        public string MeetingLink { get; set; }
        [DataMember]
        public int? Duration { get; set; }
        [DataMember]
        public int? NoOfRounds { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public string JobTitle { get; set; }

        [DataMember]
        public KeyValueDTO Interviewer { get; set; }  
        
        [DataMember]
        public string InterviewerName { get; set; }

        [DataMember]
        public List<KeyValueDTO> InterviewRounds { get; set; }
                
        [DataMember]
        public List<JobInterviewMapDTO> ShortListDTO { get; set; }

        [DataMember]
        public string Rounds { get; set; }

    }
}
