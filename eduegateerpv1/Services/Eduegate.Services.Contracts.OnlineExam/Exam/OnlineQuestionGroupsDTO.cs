using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
    public class OnlineQuestionGroupsDTO : BaseMasterDTO
    {
        public OnlineQuestionGroupsDTO()
        {
            QuestionIDs = new List<long>();
        }

        [DataMember]
        public int QuestionGroupID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string GroupName { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public decimal? TotalQuestions { get; set; }

        //Used for shuffling logic
        [DataMember]
        public List<long> QuestionIDs { get; set; }

        [DataMember]
        public bool IsErrorOccuredWhileShuffling { get; set; }

        [DataMember]
        public string QnShufflingErrorMessage { get; set; }

    }
}