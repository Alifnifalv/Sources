using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
    public class PassageQuestionDTO : BaseMasterDTO
    {
        public PassageQuestionDTO()
        {
            //QuestionIDs = new List<long>();
        }

        [DataMember]
        public long PassageQuestionIID { get; set; }

        [DataMember]
        public string PassageQuestion { get; set; }

        [DataMember]
        public string ShortDescription { get; set; }

        [DataMember]
        public decimal? MinimumMark { get; set; }

        [DataMember]
        public decimal? MaximumMark { get; set; }

    }
}