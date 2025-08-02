using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class ExamDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ExamDTO()
        {
            ExamSchedules = new ExamScheduleDTO();
            ExamSubjects = new List<ExamSubjectDTO>();
            ExamClasses = new List<ExamClassDTO>();
            ExamStudentList = new List<KeyValueDTO>();
            ExamClassList = new List<KeyValueDTO>();
            ExamSubjectList = new List<KeyValueDTO>();
            ExamSectionList = new List<KeyValueDTO>();
            ExamSkillMaps = new List<ExamSkillMapDTO>();
        }

        [DataMember]
        public long  ExamIID { get; set; }
        [DataMember]
        public string  ExamDescription { get; set; }
        [DataMember]
        public byte? ExamTypeID { get; set; }
        [DataMember]
        public int? ExamGroupID { get; set; }

        [DataMember]
        public string ExamTypeName { get; set; }

        [DataMember]
        public string ExamGroupName { get; set; }

        [DataMember]
        public int? MarkGradeID { get; set; }
        [DataMember]
        public bool?  IsActive { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
        [DataMember]
        public ExamScheduleDTO ExamSchedules { get; set; }
        [DataMember]
        public List<ExamSubjectDTO> ExamSubjects { get; set; }
        [DataMember]
        public List<ExamClassDTO> ExamClasses { get; set; }

        [DataMember]
        public List<KeyValueDTO> ExamStudentList { get; set; }

        [DataMember]
        public List<KeyValueDTO> ExamClassList { get; set; }

        [DataMember]
        public List<KeyValueDTO> ExamSectionList { get; set; }

        [DataMember]
        public List<KeyValueDTO> ExamSubjectList { get; set; }

        [DataMember]
        public List<ExamSkillMapDTO> ExamSkillMaps { get; set; }

        [DataMember]
        [StringLength(100)]
        public string ProgressCardHeader { get; set; }

        [DataMember]
        public bool? IncludeInFinalReport { get; set; }

        [DataMember]
        public bool? IncludeInInterimReport { get; set; }
    }
}


