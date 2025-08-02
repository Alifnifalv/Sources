using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "LeaveDetails", "CRUDModel.ViewModel.LeaveDetails")]
    [DisplayName("Setting Leave Types")]
    public class EmployeeLeaveDetailsViewModel : BaseMasterViewModel
    {
        public EmployeeLeaveDetailsViewModel()
        {
            LeaveTypes = new List<EmployeeLeaveTypeViewModel>() { new EmployeeLeaveTypeViewModel() };
            IsListDisable = true;
        }
        public bool IsListDisable { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='LeaveGroupChanges($event, $element,CRUDModel.ViewModel.LeaveDetails)'")]
        [LookUp("LookUps.LeaveGroup")]
        [CustomDisplay("LeaveGroup")]
        public string LeaveGroup { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft2", "ng-change=LeaveGroupChanges($event, $element,CRUDModel.ViewModel.LeaveDetails)")]
        [CustomDisplay("Override Leave Group")]
        public bool? IsOverrideLeaveGroup { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, attribs: "ng-disabled=CRUDModel.ViewModel.LeaveDetails.IsListDisable")]//, attribs:" ng-disabled=gridModel.IsListDisable"
        [CustomDisplay("Leave Types")]
        public List<EmployeeLeaveTypeViewModel> LeaveTypes { get; set; }

    }

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "LeaveTypes", "CRUDModel.ViewModel.LeaveTypes")]
    [DisplayName("Leave Types")]
    public class EmployeeLeaveTypeViewModel : BaseMasterViewModel
    {
        public EmployeeLeaveTypeViewModel()
        {
            LeaveType = new KeyValueViewModel();
        }

        public long LeaveAllocationIID { get; set; }

        public long? EmployeeSalaryStructureComponentMapID { get; set; }

        public long? EmployeeSalaryStructureID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("LeaveType", "Numeric", false, "")]
        [LookUp("LookUps.LeaveType")]
        [CustomDisplay("Leave Type")]
        public KeyValueViewModel LeaveType { get; set; }
        public int? LeaveTypeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[MaxLength(15, ErrorMessage = "Maximum Length should be within 4!")]
        [CustomDisplay("No. of days")]
        public decimal? AllocatedLeaves { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.LeaveTypes[0], CRUDModel.ViewModel.LeaveTypes)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.LeaveTypes[0],CRUDModel.ViewModel.LeaveTypes)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
