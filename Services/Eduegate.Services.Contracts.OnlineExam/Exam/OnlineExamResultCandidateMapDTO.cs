using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Domain.Entity.OnlineExam
{
    [DataContract]
    public class OnlineExamResultCandidateMapDTO : BaseMasterDTO
    {
        [DataMember]
        public long? OnlineExamSubjectMapID { get; set; }

        [DataMember]
        public long? OnlineExamResultSubjectMapID { get; set; }

        [DataMember]
        public long? OnlineExamResultID { get; set; }

        [DataMember]
        public long? OnlineExamID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public decimal? Marks { get; set; }

        [DataMember]
        public decimal? MaxMark { get; set; }
       
        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public long? CandidateID { get; set; }

        [DataMember]
        public string CandidateName { get; set; }
    }
}