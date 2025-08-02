using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class ExamScheduleDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        [DataMember]
        public long ExamScheduleIID { get; set; }

        [DataMember]
        public long? ExamID { get; set; }

        [DataMember]
        public DateTime? Date { get; set; }

        [DataMember]
        public DateTime? ExamStartDate { get; set; }

        [DataMember]
        public DateTime? ExamEndDate { get; set; }

        [DataMember]
        public TimeSpan? StartTime { get; set; }

        [DataMember]
        public TimeSpan? EndTime { get; set; }

        [DataMember]
        public string Room { get; set; }

        [DataMember]
        public double? FullMarks { get; set; }

        [DataMember]
        public double? PassingMarks { get; set; }
    }
}


