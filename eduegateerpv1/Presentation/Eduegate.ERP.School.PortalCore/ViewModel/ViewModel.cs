using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eduegate.ERP.School.ParentPortal.ViewModel
{
    public class ResponseModel
    {
        // [Bind(Prefix = "some_prefix")]
        public string Message { set; get; }
        public object Data { set; get; }
        public bool isError { get; set; }
    }
    public class STUDENT_PROGRESS_REPORT
    {
        public long ExamID { get; set; }
        public string ExamName { get; set; }
        public List<STUDENT_PROGRESS_REPORT_MARKLIST> MarkList { get; set; }
    }
    public class STUDENT_PROGRESS_REPORT_MARKLIST
    {
        public long ExamID { get; set; }
        public string ExamName { get; set; }
        public long StudentId { get; set; }
        public string StudentName { get; set; }
        public long ClassID { get; set; }
        //public string ClassName { get; set; }
        public long SectionID { get; set; }
        //public string SectionName { get; set; }
        public long ExamTypeID { get; set; }
        public string ExamTypeName { get; set; }
        public long SubjectID { get; set; }
        public string SubjectName { get; set; }
        public double MinimumMarks { get; set; }
        public double MaximumMarks { get; set; }
        public double Mark { get; set; }
        public long MarksGradeMapID { get; set; }
        public string GradeName { get; set; }
        public bool IsPassed { get; set; }

    }
    public class STUDENT_TRANSPORT_DETAILS
    {
        public long StudentID { get; set; }
        public string Name { get; set; }
        //public long? PickupStopMapID { get; set; }
        public string PickupStopMapName { get; set; }
        //public long? DropStopMapID { get; set; }
        public string DropStopMapName { get; set; }

        public string DropStopDriverName { get; set; }
        public string DropStopRouteCode { get; set; }
        public string PickupRouteCode { get; set; }

        public string PickupContactNo { get; set; }

        public string DropContactNo { get; set; }

        public string PickupStopDriverName { get; set; }
        public Boolean IsOneWay { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public long ClassID { get; set; }
        public long SectionID { get; set; }
        public long LoginID { get; set; }
    }

    public class ATTENDENCE_PERCENTAGE_BY_PARENT_LOGINID
    {
        public Decimal PercentAttendance { get; set; }
        public Decimal TotalWorkingDays { get; set; }
        public Decimal TotalPresetDays { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime AcademicStartDate { get; set; }
        public DateTime AcademicEndDate { get; set; }
        public long TotalLeave { get; set; }
        public long StudentIID { get; set; }
        public Boolean IsActive { get; set; }
        public long? LoginID { get; set; }
        public string StudentName { get; set; }
    }

    public class TEACHERS_MAIL_ID_BY_PARENT_LOGINID
    {
        public string LoginEmailID { get; set; }
        public string EmployeeName { get; set; }
        public long EmployeeLoginID { get; set; }
    }
    public class MAIL_LIST
    {
        public long mailBoxID { get; set; }
        public long fromID { get; set; }
        public long toID { get; set; }
        public string mailSubject { get; set; }
        public string mailBody { get; set; }
        public string mailFolder { get; set; }
        public bool viewStatus { get; set; }
        public bool fromDelete { get; set; }
        public bool toDelete { get; set; }
        public DateTime onDate { get; set; }
        public string UserName { get; set; }

    }

    public class AssignmentDocuments
    {
        public int StudentId { get; set; }
        public int AssignmentIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [DisplayName("Attachment")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ProfileUrl", "")]
        public string ProfileUploadFile { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Attachment Name")]
        public string AttachmentName { get; set; }


        //[Required]
        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Description")]
        public string Description { get; set; }

        //[Required]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Notes")]
        public string Notes { get; set; }
    }
}