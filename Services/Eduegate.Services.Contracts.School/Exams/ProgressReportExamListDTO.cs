using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class ProgressReportExamListDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ProgressReportExamListDTO()
        {
        }

        [DataMember]
        public long? ExamID { get; set; }

        [DataMember]
        public string ExamName { get; set; }

        [DataMember]
        public decimal? MinimumMarks { get; set; }

        [DataMember]
        public decimal? MaximumMarks { get; set; }

        [DataMember]
        public decimal? Mark { get; set; }

        [DataMember]
        public long? MarksGradeMapID { get; set; }

        [DataMember]
        public string Grade { get; set; }

        [DataMember]
        public bool? IsAbsent { get; set; }

        [DataMember]
        public bool? IsPassed { get; set; }
    }
}