using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class StudentSkillRegisterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentSkillRegisterDTO()
        {
            StudentSkillRegisterMap = new List<StudentSkillRegisterMapDTO>();
            Class = new KeyValueDTO();
            Student = new KeyValueDTO();
            Section = new KeyValueDTO();
            Exam = new KeyValueDTO();
            Subject = new KeyValueDTO();
        }
        
        [DataMember]
        public long StudentSkillRegisterIID { get; set; }

        [DataMember]
        public long? ExamID { get; set; }

        [DataMember]
        public long? StudentId { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public bool? IsAbsent { get; set; }

        [DataMember]
        public long? MarksGradeMapID { get; set; }

        [DataMember]
        public bool? IsPassed { get; set; }

        [DataMember]
        public decimal? Mark { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }

        [DataMember]
        public KeyValueDTO Exam { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]
        public List<StudentSkillRegisterMapDTO> StudentSkillRegisterMap { get; set; }
    }
}