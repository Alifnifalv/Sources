using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class MarkRegisterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public MarkRegisterDTO()
        {
            Exam = new KeyValueDTO();
            Class = new KeyValueDTO();
            Section = new KeyValueDTO();
            Subject = new KeyValueDTO();
            GradeList = new List<MarkGradeMapDTO>();
            SkillGradeList = new List<MarkGradeMapDTO>();
            SkillGrpGradeList = new List<MarkGradeMapDTO>();
            MarkRegistersDetails = new List<MarkRegisterDetailsDTO>();
            MarkRegisterSkillGroupDTOs = new List<MarkRegisterSkillGroupDTO>();
        }

        [DataMember]
        public long MarkRegisterIID { get; set; }

        [DataMember]
        public long? ExamID { get; set; }

        [DataMember]
        public int? ExamGroupID { get; set; }//SkillSetID

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

        [DataMember]
        public List<StudentMarkEntryDTO> StudentMarkEntryList { get; set; }

        [DataMember]
        public List<MarkRegisterSkillGroupDTO> MarkRegisterSkillGroupDTOs { get; set; }

    }

    [DataContract]
    public class StudentMarkEntryDTO
    {
        public StudentMarkEntryDTO()
        {
            GradeList = new List<MarkGradeMapDTO>();
        }

        [DataMember]
        public long? MarkRegisterID { get; set; }

        [DataMember]
        public long? MarkRegisterSubjectMapID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string EnrolNo { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public byte? PresentStatusID { get; set; }

        [DataMember]
        public decimal? MarksObtained { get; set; }

        [DataMember]
        public decimal? TotalPercentage { get; set; }

        [DataMember]
        public long? GradeID { get; set; }

        [DataMember]
        public decimal? TotalMark { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public decimal MaxMark { get; set; }

        [DataMember]
        public List<MarkGradeMapDTO> GradeList { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public byte? MarkEntryStatusID { get; set; }

        [DataMember]
        public string MarkEntryStatusName { get; set; }

        [DataMember]
        public byte? LanguageTypeID { get; set; }

        [DataMember]
        public byte? SubjectMapID { get; set; }

        [DataMember]
        public decimal? MarkConvertionFactor { get; set; }
    }

    [DataContract]
    public class StudentSkillsDTO
    {
        [DataMember]
        public long? MarkRegisterID { get; set; }

        [DataMember]
        public long StudentID { get; set; }

        [DataMember]
        public string EnrolNo { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public byte? PresentStatusID { get; set; }

        [DataMember]
        public decimal? MarksObtained { get; set; }

        [DataMember]
        public decimal? TotalPercentage { get; set; }

        [DataMember]
        public long? GradeID { get; set; }

        [DataMember]
        public string GradeName { get; set; }

        [DataMember]
        public int? SkillMasterID { get; set; }

        [DataMember]
        public int? SkillGroupMasterID { get; set; }

        [DataMember]
        public string SkillGroup { get; set; }

        [DataMember]
        public string Skill { get; set; }

        [DataMember]
        public long? MarkRegisterSkillGroupID { get; set; }

        [DataMember]
        public long? MarkRegisterSkillID { get; set; }

        [DataMember]
        public long? ExamID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public int? ExamGroupID { get; set; }

        [DataMember]
        public byte? MarkEntryStatusID { get; set; }

        [DataMember]
        public string MarkEntryStatusName { get; set; }
    }

    [DataContract]
    public class SkillSDataDTO
    {
        [DataMember]
        public string SkillGroup { get; set; }

        [DataMember]
        public string Skill { get; set; }

        [DataMember]
        public int? SkillGroupMasterID { get; set; }

        [DataMember]
        public int? SkillMasterID { get; set; }

        [DataMember]
        public int MarkGradeID { get; set; }

        [DataMember]
        public byte? MarkEntryStatusID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public short? SubjectTypeID { get; set; }

        [DataMember]
        public decimal? ConvertionFactor { get; set; }

        [DataMember]
        public decimal? MinimumMarks { get; set; }

        [DataMember]
        public decimal? MaximumMarks { get; set; }
    }

}