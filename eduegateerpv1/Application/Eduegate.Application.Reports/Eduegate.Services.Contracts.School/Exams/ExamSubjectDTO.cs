
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class ExamSubjectDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ExamSubjectDTO()
        {
            SubjectType = new KeyValueDTO();
        }

        [DataMember]
        public long ExamSubjectMapIID { get; set; }

        [DataMember]
        public long? ExamID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }


        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string MarkGrade { get; set; }

        [DataMember]
        public DateTime? ExamDate { get; set; }

        [DataMember]
        public decimal? MinimumMarks { get; set; }

        [DataMember]
        public decimal? MaximumMarks { get; set; }

        [DataMember]
        public TimeSpan? StartTime { get; set; }

        [DataMember]
        public TimeSpan? EndTime { get; set; }

        [DataMember]
        public int? MarkGradeID { get; set; }

        [DataMember]
        public KeyValueDTO SubjectType { get; set; }

        [DataMember]
        public decimal? ConversionFactor { get; set; }

    }
}