using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabEmployeeLeaveDatails", "CRUDModel.ViewModel.MCTabEmployeeLeaveDatails")]
    [DisplayName("EmployeeLeaveDatails")]
    public class TabEmployeeLeaveDatailsViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", attribs: "ng-disabled=true")]
        [CustomDisplay("Date Of Joining")]
        public string DateOfJoiningString { get; set; }
        public System.DateTime? DateOfJoining { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", attribs: "ng-disabled=true")]
        [CustomDisplay("Annual Leave Entitilements")]
        public decimal? AnnualLeaveEntitilements { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine12 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", attribs: "ng-disabled=true")]
        [CustomDisplay("Leave Due From")]
        public string LeaveDueFromString { get; set; }
        public System.DateTime? LeaveDueFrom { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", attribs: "ng-disabled=true")]
        [CustomDisplay("No of Salary Days")]
        public decimal? NoofSalaryDays { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", attribs: "ng-disabled=true")]
        [CustomDisplay("Leave Start Date")]
        public string LeaveStartDateString { get; set; }
        public System.DateTime? LeaveStartDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", attribs: "ng-disabled=true")]
        [CustomDisplay("No of days in the month")]
        public decimal? NoofDaysInTheMonth { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", attribs: "ng-disabled=true")]
        [CustomDisplay("Leave End Date")]
        public string LeaveEndDateString { get; set; }
        public System.DateTime? LeaveEndDate { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[CustomDisplay("Loss of Pay (Days)")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", attribs: "ng-disabled=true")]
        //[CustomDisplay("Earned Leave")]
        public decimal? EarnedLeave { get; set; }
        public decimal? LossofPay { get; set; }


    }
}
