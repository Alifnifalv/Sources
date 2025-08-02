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

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ESBProvisionDetail", "CRUDModel.ViewModel.ESBProvisionTab.ESBProvisionDetail")]
    [DisplayName("Detail")]
    [Pagination(10, "default")]
    public class ESBProvisionDetailsViewModel : BaseMasterViewModel
    {
        public long? EmployeeESBProvisionHeadID { get; set; }
        public long EmployeeESBProvisionDetailIID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("EmployeeCode")]
        public string EmployeeCode { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Employee")]
        public string EmployeeName { get; set; }
        
        public long? EmployeeID { get; set; }
        
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
       
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Basic")]
        public decimal? BasicSalary { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Opening Amount")]
        public decimal? OpeningAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("ESB")]
        public decimal? ESBAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Balance")]
        public decimal? Balance { get; set; }


    }
}
