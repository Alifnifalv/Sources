using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;

namespace Eduegate.Web.Library.School.Exams
{
    public class MarkApprovalViewModel
    {
        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? ClassID { get; set; }

        public KeyValueViewModel StudentClass { get; set; }

        public int? SubjectID { get; set; }

        public int? ExamGroupID { get; set; }

        public int? SectionID { get; set; }

        public long? ExamID { get; set; }

        public string EnrollNumber { get; set; }

        public KeyValueViewModel Student { get; set; }

        public long? StudentID { get; set; }

        public bool IsSelected { get; set; }

        public byte? MarkEntryStatusID { get; set; }

        public KeyValueViewModel MarkEntryStatus { get; set; }

        public byte? PreviousMarkEntryStatusID { get; set; }

        public string StudentFeeDefaulterStatus { get; set; }
        public long? ReportContentID { get; set; }

        public List<MarkApprovalMarkListViewModel> StudentMarkList { get; set; }

        public List<MarkApprovalSkillListViewModel> StudentSkillList { get; set; }

    }

    public class MarkApprovalMarkListViewModel
    {
        public long? MarkRegisterID { get; set; }

        public long? MarkRegisterSubjectMapID { get; set; }

        public long? MarksGradeMapID { get; set; }

        public long StudentID { get; set; }

        public string StudentName { get; set; }

        public string EnrollNumber { get; set; }

        public string PresentStatusID { get; set; }

        public decimal? MarksObtained { get; set; }

        public decimal? TotalPercentage { get; set; }

        public string GradeID { get; set; }

        public string GradeKey { get; set; }

        public decimal? TotalMark { get; set; }

        public decimal MaxMark { get; set; }

        public int? SubjectID { get; set; }

        public string SubjectName { get; set; }

        public bool IsSelected { get; set; }

        public byte? MarkEntryStatusID { get; set; }

        public KeyValueViewModel MarkEntryStatus { get; set; }

        public List<MarkListSkillGroupViewModel> SkillGroup { get; set; }

    }

    public class MarkApprovalSkillListViewModel
    {
        public long StudentID { get; set; }

        public string StudentName { get; set; }

        public long MarkRegisterSkillGroupIID { get; set; }
        
        public long? MarkRegisterSubjectMapID { get; set; }
        
        public long? MarkRegisterID { get; set; }
       
        public decimal? MinimumMark { get; set; }
        
        public decimal? MaximumMark { get; set; }
        
        public decimal? MarkObtained { get; set; }
        
        public int? MarksGradeID { get; set; }
        
        public long? MarksGradeMapID { get; set; }
        
        public string Grade { get; set; }
        
        public int? SkillGroupMasterID { get; set; }
       
        public string MarkGradeMap { get; set; }
        
        public string SkillGroup { get; set; }
        
        public bool? IsPassed { get; set; }
       
        public bool? IsAbsent { get; set; }

        public byte? MarkEntryStatusID { get; set; }

        public KeyValueViewModel MarkEntryStatus { get; set; }

        public List<MarkListSkillGroupSkillViewModel> SkillList { get; set; }

    }

    public class MarkListSkillGroupViewModel
    {
        public long MarkRegisterSkillGroupIID { get; set; }
        
        public long? MarkRegisterSubjectMapID { get; set; }
        
        public long? MarkRegisterID { get; set; }
       
        public decimal? MinimumMark { get; set; }
        
        public decimal? MaximumMark { get; set; }
        
        public decimal? MarkObtained { get; set; }
        
        public int? MarksGradeID { get; set; }
        
        public long? MarksGradeMapID { get; set; }
        
        public string Grade { get; set; }
        
        public int? SkillGroupMasterID { get; set; }
        
        public string MarkGradeMap { get; set; }
        
        public string SkillGroup { get; set; }
       
        public bool? IsPassed { get; set; }
        
        public bool? IsAbsent { get; set; }

        public byte? MarkEntryStatusID { get; set; }

        public KeyValueViewModel MarkEntryStatus { get; set; }

        public List<MarkListSkillGroupSkillViewModel> Skills { get; set; }

    }

    public class MarkListSkillGroupSkillViewModel
    {
        public long MarkRegisterSkillIID { get; set; }
        
        public long? MarkRegisterSkillGroupID { get; set; }
        
        public long? MarkRegisterID { get; set; }
        
        public int? SkillMasterID { get; set; }
        
        public decimal? MarksObtained { get; set; }
        
        public long? MarksGradeMapID { get; set; }
       
        public int? SkillGroupMasterID { get; set; }
        
        public decimal? MinimumMark { get; set; }
        
        public decimal? MaximumMark { get; set; }
        
        public string MarkGradeMap { get; set; }
        
        public int? MarksGradeID { get; set; }
        
        public string SkillGroup { get; set; }
        
        public string Skill { get; set; }
        
        public bool? IsPassed { get; set; }
       
        public bool? IsAbsent { get; set; }
        
        public string Grade { get; set; }

        public byte? MarkEntryStatusID { get; set; }

        public KeyValueViewModel MarkEntryStatus { get; set; }

    }

}