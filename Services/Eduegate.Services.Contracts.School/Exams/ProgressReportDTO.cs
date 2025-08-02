using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class ProgressReportDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ProgressReportDTO()
        {
            Class = new KeyValueDTO();
            Student = new KeyValueDTO();
            Section = new KeyValueDTO();
        }

        [DataMember]
        public long ProgressReportIID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public string SchoolName { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public long? ReportContentID { get; set; }

        [DataMember]
        public string ReportContentFileName { get; set; }

        [DataMember]
        public byte? PublishStatusID { get; set; }

        [DataMember]
        public string PublishStatusName { get; set; }

        [DataMember]
        public long? ExamID { get; set; }

        [DataMember]
        public string ExamName { get; set; }

        [DataMember]
        public int? ExamGroupID { get; set; }

        [DataMember]
        public string ExamGroup { get; set; }

        [DataMember]
        public List<ProgressReportExamListDTO> Exams { get; set; }

        [DataMember]
        public List<ProgressReportSubjectListDTO> Subjects { get; set; }
    }
}