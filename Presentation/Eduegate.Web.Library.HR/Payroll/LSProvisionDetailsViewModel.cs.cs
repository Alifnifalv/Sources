using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "LSProvisionDetail", "CRUDModel.ViewModel.LSProvisionTab.LSProvisionDetail")]
    [DisplayName("Detail")]
    [Pagination(10, "default")]
    public class LSProvisionDetailsViewModel : BaseMasterViewModel
    {
        public LSProvisionDetailsViewModel()
        {
           
        }
        public long? EmployeeLSProvisionHeadID { get; set; }
        public long EmployeeLSProvisionDetailIID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("EmployeeCode")]
        public string EmployeeCode { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Employee")]
        public string EmployeeName { get; set; }
        [DataMember]
        public long? EmployeeID { get; set; }
        [DataMember]
        public int? DepartmentID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Department")]
        public string Department { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Category")]
        public string Category { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Date Of Joining")]
        public string DOJString { get; set; }
        public DateTime? DOJ { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", optionalAttribs: "ng-disabled=true")]
        
        [CustomDisplay("LS Settlement Last Date")]
        public string LastLeaveSalaryDateString { get; set; }
        public DateTime? LastLeaveSalaryDate { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Basic")]
        public decimal? BasicSalary { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("LS Days")]
        public decimal? NoofLeaveSalaryDays { get; set; }    

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Opening Amount")]
        public decimal? OpeningAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Leave Salary")]
        public decimal? LeaveSalaryAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Balance")]
        public decimal? Balance { get; set; }       

    }
}
