using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "TimeSheets", "CRUDModel.ViewModel.TimeSheets")]
    [DisplayName("")]
    public class EmployeeTimeSheetsTimeViewModel : BaseMasterViewModel
    {
        public EmployeeTimeSheetsTimeViewModel()
        {
            Task = new KeyValueViewModel();
        }

        public long EmployeeTimeSheetIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Task", "Numeric", false, "")]
        [LookUp("LookUps.Task")]
        [CustomDisplay("Task")]
        public KeyValueViewModel Task { get; set; }
        public long TaskID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, Attributes = "ng-change='TimesheetTimeChanges(CRUDModel.ViewModel, gridModel)'")]
        [CustomDisplay("TimesheetDate")]
        public string TimesheetDateString { get; set; }
        public DateTime TimesheetDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TimePicker, Attributes = "ng-change='TimesheetTimeChanges(CRUDModel.ViewModel, gridModel)'")]
        [CustomDisplay("Start Time")]
        public string FromTimeString { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TimePicker, Attributes = "ng-change='TimesheetTimeChanges(CRUDModel.ViewModel, gridModel)'")]
        [CustomDisplay("End Time")]
        public string ToTimeString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, Attributes2 = "ng-change='TimesheetTimeTypeChanges(CRUDModel.ViewModel, gridModel)'")]
        [LookUp("LookUps.TimesheetTimeType")]
        [CustomDisplay("Time Type")]
        public string TimesheetTimeType { get; set; }
        public byte? TimesheetTimeTypeID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [CustomDisplay("NormalHours")]
        public decimal? NormalHours { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [CustomDisplay("OverTimeHours")]
        public decimal? OTHours { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.TimesheetEntryStatus")]
        [CustomDisplay("Status")]
        public string EntryStatus { get; set; } 
        public byte? TimesheetEntryStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        public decimal? OldNormalHours { get; set; }

        public decimal? OldOTHours { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.TimeSheets[0], CRUDModel.ViewModel.TimeSheets)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.TimeSheets[0],CRUDModel.ViewModel.TimeSheets)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}