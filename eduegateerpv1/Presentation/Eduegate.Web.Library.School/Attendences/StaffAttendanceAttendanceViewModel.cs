using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using Eduegate.Domain;

namespace Eduegate.Web.Library.School.Attendences
{
    [ContainerType(Framework.Enums.ContainerTypes.FieldSet, "StaffAttendance", "CRUDModel.ViewModel.StaffAttendance")]
    public class StaffAttendanceAttendanceViewModel : BaseMasterViewModel
    {
        public StaffAttendanceAttendanceViewModel()
        {
            StaffPresentStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STAFFSTATUSID_PRESENT");
        }

        public string StaffPresentStatusID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("AttendanceDate")]
        public string AttendenceDateString { get; set; }
        public System.DateTime? AttendenceDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("PresentStatus")]
        [LookUp("LookUps.PresentStaffStatus")]
        public string PresentStatus { get; set; }
        public byte? PresentStatusID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown,attribs: "ng-disabled=CRUDModel.ViewModel.StaffAttendance.PresentStatus==CRUDModel.ViewModel.StaffAttendance.StaffPresentStatusID")]
        [CustomDisplay("Reason")]
        [LookUp("LookUps.AttendenceReason")]
        public string AttendenceReason { get; set; }
        public int? AttendenceReasonID { get; set; }

        //[Required]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "doublewidth alignleft", attribs: "ng-disabled=CRUDModel.ViewModel.StaffAttendance.PresentStatus==CRUDModel.ViewModel.StaffAttendance.StaffPresentStatusID")]
        [CustomDisplay("Notes")]
        public string Notes { get; set; }
    }
}