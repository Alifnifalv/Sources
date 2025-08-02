using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;

namespace Eduegate.Web.Library.School.Exams
{
    public class MarksEntryViewModel
    {
        public MarksEntryViewModel() {

            Students = new List<StudentCoScholasticEntryViewModel>() { new StudentCoScholasticEntryViewModel() };
            MarkEntryStudents = new List<StudentMarkEntryViewModel>() { new StudentMarkEntryViewModel() };
        }

        public int? AcademicYearID { get; set; }
        public int? ClassID { get; set; }
        public int? SubjectID { get; set; }
        public int? ExamGroupID { get; set; }
        public byte? LanguageTypeID { get; set; }
        public byte? SubjectMapID { get; set; }
        public int? ExamID { get; set; }
        public int? SectionId { get; set; }
        public int? SkillGroupID { get; set; }
        public int? SkillSetID { get; set; }
        public int? SkillID { get; set; }
        public List<StudentCoScholasticEntryViewModel> Students { get; set; }
        public List<StudentMarkEntryViewModel> MarkEntryStudents { get; set; }
        public bool IsError { get; set; }
    }

    public class StudentMarkEntryViewModel
    {
        public long? MarkRegisterID { get; set; }
        public long? MarkRegisterSubjectMapID { get; set; }
        public long? StudentID { get; set; }
        public string EnrolNo { get; set; }
        public string StudentName { get; set; }
        public string MarkStatusID { get; set; }
        public decimal? MarksObtained { get; set; }
        public decimal? TotalPercentage { get; set; }
        public string GradeID { get; set; }
        public string GradeKey { get; set; }
        public decimal? TotalMark { get; set; }
        public decimal MaxMark { get; set; }
        public int? SubjectID { get; set; }
        public string SubjectName { get; set; }
        public decimal? MarkConvertionFactor { get; set; }
        public bool? IsReportPublished { get; set; }
    }

    public class StudentCoScholasticEntryViewModel
    {
        public StudentCoScholasticEntryViewModel() {
            SkillGroups = new List<SkillGroupCoScholasticEntryViewModel>() { new SkillGroupCoScholasticEntryViewModel() };
        }

        public long StudentIID { get; set; }
        public string EnrolNo { get; set; }
        public string StudentName { get; set; }
        public string MarkStatusID { get; set; }
        public int IsEdited { get; set; }
        public bool? IsReportPublished { get; set; }

        public List<SkillGroupCoScholasticEntryViewModel> SkillGroups { get; set; }
    }

    public class SkillGroupCoScholasticEntryViewModel
    {
        public SkillGroupCoScholasticEntryViewModel()
        {
            Skills = new List<SkillCoScholasticEntryViewModel>() { new SkillCoScholasticEntryViewModel() };
        }

        public long SkillGroupID { get; set; }
        public List<SkillCoScholasticEntryViewModel> Skills { get; set; }
        public int IsEditedSkill { get; set; }
    }
    public class SkillCoScholasticEntryViewModel
    {
        public SkillCoScholasticEntryViewModel()
        {
            MarkGradeMapList = new List<KeyValueViewModel>() { new KeyValueViewModel() };
            GradeMarkRangeList = new List<MarkGradeMapViewModel> { new MarkGradeMapViewModel() };
        }

        public int? SkillID { get; set; }
        public string SkillName { get; set; }

        public string GradeID { get; set; }
        public string GradeKey { get; set; }

        public long? MarkRegisterSkillID { get; set; }

        public long? MarkRegisterSkillGroupID { get; set; }

        public long? MarkRegisterID { get; set; }

        public List<KeyValueViewModel> MarkGradeMapList { get; set; }

        public List<MarkGradeMapViewModel> GradeMarkRangeList { get; set; }

        public decimal? MarksObtained { get; set; }
        public decimal? TotalPercentage { get; set; }
        public decimal? TotalMark { get; set; }
        public decimal? MaxMark { get; set; }

        public decimal? ConvertionFactor { get; set; }
    }

}