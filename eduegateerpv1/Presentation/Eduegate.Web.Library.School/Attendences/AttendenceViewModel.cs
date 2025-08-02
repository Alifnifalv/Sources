using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Attendences
{
    [ContainerType(Framework.Enums.ContainerTypes.FieldSet, "Attendence", "CRUDModel.ViewModel.Attendence")]
    public class AttendenceViewModel : BaseMasterViewModel
    {
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
        [LookUp("LookUps.PresentStatus")]
        public string PresentStatus { get; set; }
        public byte? PresentStatusID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown,attribs: "ng-disabled=CRUDModel.ViewModel.Attendence.PresentStatus==6")]
        [CustomDisplay("Reason")]
        [LookUp("LookUps.AttendenceReason")]
        public string AttendenceReason { get; set; }
        public int? AttendenceReasonID { get; set; }

        //[Required]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "doublewidth alignleft", attribs: "ng-disabled=CRUDModel.ViewModel.Attendence.PresentStatus==6")]
        [CustomDisplay("Notes")]
        public string Notes { get; set; }
    }
}