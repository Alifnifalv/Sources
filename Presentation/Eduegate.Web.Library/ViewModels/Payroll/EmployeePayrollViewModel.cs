using Eduegate.Framework.Mvc.Attributes;
using System.ComponentModel;
using Eduegate.Web.Library.Common;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.ViewModels.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Payroll", "CRUDModel.ViewModel.Payroll")]
    [DisplayName("Payroll")]
    public class EmployeePayrollViewModel : BaseMasterViewModel
    {
        public EmployeePayrollViewModel()
        {
            IsOTEligible = false;
            IsLeaveSalaryEligible=false;
            IsEoSBEligible=false;
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='CalendarTypeChanges($event, $element,CRUDModel.ViewModel.Payroll)'")]
        [LookUp("LookUps.CalendarType")]
        [CustomDisplay("Category")]
        public string CalendarType { get; set; }
        public byte? CalendarTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.CalendarMasters")]
        [CustomDisplay("Calendar")]
        public string AcademicCalendar { get; set; }
        public long? AcademicCalendarID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsEligibleforOverTime")]
        public bool? IsOTEligible { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Is Leave Salary Eligible")]
        public bool? IsLeaveSalaryEligible { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Is EoSB Eligible")]
        public bool? IsEoSBEligible { get; set; }
    }
}