using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "TimeSheetTimeDetails", "CRUDModel.ViewModel.TimeSheetTimeDetails")]
    [DisplayName("")]
    public class EmployeeTimeSheetApprovalTimeViewModel : BaseMasterViewModel
    {
        public EmployeeTimeSheetApprovalTimeViewModel()
        {
        }

        public long EmployeeTimeSheetIID { get; set; }

        public long TaskID { get; set; }
        public string TaskName { get; set; }

        public string FromTimeString { get; set; }

        public string ToTimeString { get; set; }

        public decimal? NormalHours { get; set; }

        public decimal? OTHours { get; set; }

        public string Remarks { get; set; }

        public byte? TimesheetTimeTypeID { get; set; }
        public string TimesheetTimeTypeName { get; set; }
    }
}