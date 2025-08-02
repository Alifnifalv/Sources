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
        public Int64 ExamID { get; set; }
        public String ExamName { get; set; }
        public List<STUDENT_PROGRESS_REPORT_MARKLIST> MarkList { get; set; }
    }
    public class STUDENT_PROGRESS_REPORT_MARKLIST
    {
        public Int64 ExamID { get; set; }
        public String ExamName { get; set; }
        public Int64 StudentId { get; set; }
        public String StudentName { get; set; }
        public Int64 ClassID { get; set; }
        //public String ClassName { get; set; }
        public Int64 SectionID { get; set; }
        //public String SectionName { get; set; }
        public Int64 ExamTypeID { get; set; }
        public String ExamTypeName { get; set; }
        public Int64 SubjectID { get; set; }
        public String SubjectName { get; set; }
        public Double MinimumMarks { get; set; }
        public Double MaximumMarks { get; set; }
        public Double Mark { get; set; }
        public Int64 MarksGradeMapID { get; set; }
        public String GradeName { get; set; }
        public bool IsPassed { get; set; }

    }
    public class STUDENT_TRANSPORT_DETAILS
    {
        public Int64 StudentID { get; set; }
        public String Name { get; set; }
        //public Int64? PickupStopMapID { get; set; }
        public String PickupStopMapName { get; set; }
        //public Int64? DropStopMapID { get; set; }
        public String DropStopMapName { get; set; }

        public String DropStopDriverName { get; set; }
        public String DropStopRouteCode { get; set; }
        public String PickupRouteCode { get; set; }

        public String PickupContactNo { get; set; }

        public String DropContactNo { get; set; }

        public String PickupStopDriverName { get; set; }
        public Boolean IsOneWay { get; set; }
        public String DateFrom { get; set; }
        public String DateTo { get; set; }
        public Int64 ClassID { get; set; }
        public Int64 SectionID { get; set; }
        public Int64 LoginID { get; set; }
    }

    public class ATTENDENCE_PERCENTAGE_BY_PARENT_LOGINID
    {
        public Decimal PercentAttendance { get; set; }
        public Decimal TotalWorkingDays { get; set; }
        public Decimal TotalPresetDays { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime AcademicStartDate { get; set; }
        public DateTime AcademicEndDate { get; set; }
        public Int64 TotalLeave { get; set; }
        public Int64 StudentIID { get; set; }
        public Boolean IsActive { get; set; }
        public Int64? LoginID { get; set; }
        public String StudentName { get; set; }
    }

    public class TEACHERS_MAIL_ID_BY_PARENT_LOGINID
    {
        public String LoginEmailID { get; set; }
        public String EmployeeName { get; set; }
        public Int64 EmployeeLoginID { get; set; }
    }
    public class MAIL_LIST
    {
        public Int64 mailBoxID { get; set; }
        public Int64 fromID { get; set; }
        public Int64 toID { get; set; }
        public String mailSubject { get; set; }
        public String mailBody { get; set; }
        public String mailFolder { get; set; }
        public bool viewStatus { get; set; }
        public bool fromDelete { get; set; }
        public bool toDelete { get; set; }
        public DateTime onDate { get; set; }
        public String UserName { get; set; }

    }

    public class AssignmentDocuments
    {
        public int StudentId { get; set; }
        public int AssignmentIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [DisplayName("Attachment")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ProfileUrl")]
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