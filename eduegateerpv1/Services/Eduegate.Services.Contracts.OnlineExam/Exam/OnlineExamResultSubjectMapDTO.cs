using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
   public class OnlineExamResultSubjectMapDTO : BaseMasterDTO
    {
        [DataMember]
        public long OnlineExamSubjectMapIID { get; set; }

        [DataMember]
        public long? OnlineExamID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public decimal? Marks { get; set; }

        [DataMember]
        public decimal? Remarks { get; set; }
    } 
}