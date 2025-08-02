using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Jobs
{
    public class JobInterviewMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public JobInterviewMapDTO()
        {
            RoundMapDTO = new List<JobInterviewRoundMapDTO>();
        }

        [DataMember]
        public long MapID { get; set; }
        [DataMember]
        public long? InterviewID { get; set; }
        [DataMember]
        public TimeSpan? StartTime { get; set; }
        [DataMember]
        public TimeSpan? EndTime { get; set; }
        [DataMember]
        public int? CompletedRoundID { get; set; }
        [DataMember]
        public bool? IsSelected { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public long? ApplicantID { get; set; }
        [DataMember]
        public string ApplicantName { get; set; }
        [DataMember]
        public string Education { get; set; }
        [DataMember]
        public bool? InterviewAccepted { get; set; }
        [DataMember]
        public string ApplicantMailID { get; set; }

        [DataMember]
        public int? TotalRating { get; set; }

        [DataMember]
        public int? TotalRatingGot { get; set; }

        [DataMember]
        public int? TotalRounds { get; set; }
        [DataMember]
        public int? RoundsCompleted { get; set; }

        [DataMember]
        public string TotalRatingEarned { get; set; }


        //For Recuirement portal
        [DataMember]
        public string JobTitle { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string InterviewDateString { get; set; }
        [DataMember]
        public string StartTimeString { get; set; }
        [DataMember]
        public string EndTimeString { get; set; } 
        [DataMember]
        public string Interviewer { get; set; } 
        [DataMember]
        public string NextRound { get; set; }

        [DataMember]
        public List<JobInterviewRoundMapDTO> RoundMapDTO { get; set; }

        public class JobInterviewRoundMapDTO()
        {
            [DataMember]
            public int? RoundID { get; set; }
            
            [DataMember]
            public string Round { get; set; }

            [DataMember]
            public string HeldOnDateString { get; set; }   
            
            [DataMember]
            public DateTime? HeldOnDate{ get; set; } 

            [DataMember]
            public int? MaximumRating { get; set; }

            [DataMember]
            public int? Rating { get; set; }

            [DataMember]
            public string Grade { get; set; }  
            
            [DataMember]
            public string Remarks { get; set; }
        }

    }
}
