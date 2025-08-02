using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{

    public class TimeSheetsViewModel : BaseMasterViewModel
    {
        public TimeSheetsViewModel()
        {
            
            Employee = new KeyValueViewModel();
            Task = new KeyValueViewModel();
        }
        
        public long EmployeeTimeSheetIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Timesheet date ")]
        public string TimesheetDateString { get; set; }
        public DateTime TimesheetDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Employee", "Numeric", false, "")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        [DisplayName("Employee Name")]
        public KeyValueViewModel Employee { get; set; }
        public long EmployeeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Task", "Numeric", false, "")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        [DisplayName("Task")]
        public KeyValueViewModel Task { get; set; }
        public long TaskID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(4, ErrorMessage = "Maximum Length should be within 4!")]
        [DisplayName("Normal Hours")]
        public decimal? NormalHours { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(4, ErrorMessage = "Maximum Length should be within 4!")]
        [DisplayName("OT Hours")]
        public decimal? OTHours { get; set; }

        
    }
}
