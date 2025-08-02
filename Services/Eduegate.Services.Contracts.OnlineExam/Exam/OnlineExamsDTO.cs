using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
    public class OnlineExamsDTO : BaseMasterDTO
    {
        public OnlineExamsDTO()
        {
            OnlineExamQuestionGroupMaps = new List<OnlineExamQuestionGroupMapDTO>();
            Subjects = new List<KeyValueDTO>();
        }

        [DataMember]
        public long OnlineExamIID { get; set; }

        [DataMember]
        [StringLength(100)]
        public string Name { get; set; }

        [DataMember]
        [StringLength(500)]
        public string Description { get; set; }

        [DataMember]
        public byte? QuestionSelectionID { get; set; }

        [DataMember]
        public string QuestionSelectionName { get; set; }

        [DataMember]
        public double? MinimumDuration { get; set; }

        [DataMember]
        public double? MaximumDuration { get; set; }

        [DataMember]
        public double? PassPercentage { get; set; }

        [DataMember]
        public int? PassNos { get; set; }

        [DataMember]
        public decimal? MaximumMarks { get; set; }

        [DataMember]
        public decimal? MinimumMarks { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public KeyValueDTO Classes { get; set; }

        [DataMember]
        public List<KeyValueDTO> Subjects { get; set; }

        [DataMember]
        public List<OnlineExamQuestionGroupMapDTO> OnlineExamQuestionGroupMaps { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public byte? OnlineExamTypeID { get; set; }

        [DataMember]
        public decimal? TotalQnGroupsInExam { get; set; }

        [DataMember]
        public decimal? ObjectiveMarks { get; set; }

        [DataMember]
        public decimal? SubjectiveMarks { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]
        public KeyValueDTO MarkGrade { get; set; }

        [DataMember]
        public string SubTotalQuestions { get; set; }

        [DataMember]
        public string ObjTotalQuestions { get; set; }
    }
}