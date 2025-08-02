using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
    public class OnlineExamResultDTO : BaseMasterDTO
    {
        public OnlineExamResultDTO()
        {
            OnlineExamResultSubjectMapDTOs = new List<OnlineExamResultSubjectMapDTO>();
            OnlineExamResultQuestionMaps = new List<OnlineExamResultQuestionMapDTO>();
        }

        [DataMember]
        public long OnlineExamResultIID { get; set; }

        [DataMember]
        public decimal? Marks { get; set; }

        [DataMember]
        public decimal? MaxMark { get; set; }

        [DataMember]
        public long? CandidateID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public long? OnlineExamID { get; set; }

        [DataMember]
        public string CandidateName  { get; set; }

        [DataMember]
        public string SchoolName { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public string OnlineExamName { get; set; }

        [DataMember]
        public byte? ResultStatusID { get; set; }

        [DataMember]
        public string ResultStatus { get; set; }

        [DataMember]
        public List<OnlineExamResultSubjectMapDTO> OnlineExamResultSubjectMapDTOs { get; set; }

        [DataMember]
        public List<OnlineExamResultQuestionMapDTO> OnlineExamResultQuestionMaps { get; set; }
    }
}