using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class MarkListViewDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public MarkListViewDTO()
        {
            Exam = new KeyValueDTO();
            Class = new KeyValueDTO();
            Section = new KeyValueDTO();
            Student = new KeyValueDTO();
            MarkSubjectDetails = new List<MarkListSubjectMapDTO>();
        }
        [DataMember]
        public long MarkRegisterIID { get; set; }
        [DataMember]
        public long? ExamID { get; set; }
        [DataMember]
        public int? ClassID { get; set; }
        [DataMember]
        public int? SectionID { get; set; }
        [DataMember]
        public long? StudentID { get; set; }
        [DataMember]
        public decimal? Mark { get; set; }

        [DataMember]
        public KeyValueDTO Exam { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public List<MarkListSubjectMapDTO> MarkSubjectDetails { get; set; }
    }
}
