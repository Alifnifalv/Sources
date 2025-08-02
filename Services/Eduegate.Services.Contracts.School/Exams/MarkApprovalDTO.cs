using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class MarkApprovalDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public MarkApprovalDTO()
        {
            Exam = new KeyValueDTO();
            Class = new KeyValueDTO();
            Section = new KeyValueDTO();
            Subject = new KeyValueDTO();
            GradeList = new List<MarkGradeMapDTO>();
            SkillGradeList = new List<MarkGradeMapDTO>();
            SkillGrpGradeList = new List<MarkGradeMapDTO>();
            MarkRegistersDetails = new List<MarkRegisterDetailsDTO>();           
        }

        [DataMember]
        public long MarkRegisterIID { get; set; }
        [DataMember]
        public long? ExamID { get; set; }
        public int? ExamGroupID { get; set; }
        [DataMember]
        public int? ClassID { get; set; }
        [DataMember]
        public int? SectionID { get; set; }
        [DataMember]
        public int? SubjectID { get; set; }
        [DataMember]
        public decimal? Mark { get; set; }
        [DataMember]
        public byte? SchoolID { get; set; }
        [DataMember]
        public int? SkillGroupID { get; set; }

        [DataMember]
        public long? SkillSetID { get; set; }
        [DataMember]
        public int? SkillID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public KeyValueDTO Exam { get; set; }

        [DataMember]
        public KeyValueDTO PresentStatus { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]
        public List<MarkRegisterDetailsDTO> MarkRegistersDetails { get; set; }//Student's data

        [DataMember]
        public List<MarkGradeMapDTO> GradeList { get; set; }
        [DataMember]
        public List<MarkGradeMapDTO> SkillGradeList { get; set; }
        [DataMember]
        public List<MarkGradeMapDTO> SkillGrpGradeList { get; set; }

        public List<StudentMarkEntryDTO> StudentMarkEntryList { get; set; }
       
    }  
}