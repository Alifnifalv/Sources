using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class ClassSubjectSkillGroupMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ClassSubjectSkillGroupMapDTO()
        {
            SubSkills = new List<ClassSubjectSkillGroupSkillMapDTO>();
            Class = new KeyValueDTO();
            Exam= new KeyValueDTO();
            Subjects = new List<KeyValueDTO>();
            MarkGrade = new KeyValueDTO();
            //SkillGroup = new KeyValueDTO();
        }

        [DataMember]
        public long ClassSubjectSkillGroupMapID { get; set; }

        //[DataMember]
        //public int SkillGroupMasterID { get; set; }


        //[DataMember]
        //public KeyValueDTO SkillGroup { get; set; }

        [DataMember]
        public long? ExamID { get; set; }
        
        [DataMember]
        public decimal? MinimumMarks { get; set; }
        
        [DataMember]
        public decimal? MaximumMarks { get; set; }

        [DataMember]
        public KeyValueDTO Exam{ get; set; }

        [DataMember]
        public int ClassID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public List<KeyValueDTO> Subjects { get; set; }

        [DataMember]
        public int? MarkGradeID { get; set; }

        [DataMember]
        public KeyValueDTO MarkGrade { get; set; }

        [DataMember]
        public decimal? TotalMarks { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public List<ClassSubjectSkillGroupSkillMapDTO> SubSkills { get; set; }

        [DataMember]
        public decimal? ExamMinimumMark { get; set; }

        [DataMember]
        public decimal? ExamMaximumMark { get; set; }

        [DataMember]
        public bool? ISScholastic { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ProgressCardHeader { get; set; }

        [DataMember]
        public decimal? ConversionFactor { get; set; }

    }
}