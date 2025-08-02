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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "LeaveDetails", "CRUDModel.ViewModel.PromotionLeaveDetails")]
    [DisplayName("Setting Leave Types")]
    public  class EmployeePromotionLeaveDetailsViewModel : BaseMasterViewModel
    {
        public EmployeePromotionLeaveDetailsViewModel()
        {
            OldLeaveGroup = new KeyValueViewModel();
            NewLeaveGroup = new KeyValueViewModel();
            LeaveTypes = new List<EmployeePromotionLeaveTypeViewModel>() { new EmployeePromotionLeaveTypeViewModel() };
            IsListDisable = true;
        }
        public bool IsListDisable { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("LeaveGroup", "Numeric", false, "LeaveGroupChanges($event, $element, CRUDModel.ViewModel.PromotionLeaveDetails)")]
        [LookUp("LookUps.LeaveGroup")]
        [CustomDisplay("Current Leave Group")]
        public KeyValueViewModel OldLeaveGroup { get; set; }

        public Nullable<int> OldLeaveGroupID { get; set; }


        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("LeaveGroup", "Numeric", false, "LeaveGroupChanges($event, $element, CRUDModel.ViewModel.PromotionLeaveDetails)")]
        [LookUp("LookUps.LeaveGroup")]
        [CustomDisplay("New Leave Group")]
        public KeyValueViewModel NewLeaveGroup { get; set; }

        public Nullable<int> NewLeaveGroupID { get; set; }
               

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft2", "ng-change=LeaveGroupChanges($event, $element,CRUDModel.ViewModel.PromotionLeaveDetails)")]
        [CustomDisplay("Override Leave Group")]
        public bool? IsOverrideLeaveGroup { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, attribs: "ng-disabled=CRUDModel.ViewModel.PromotionLeaveDetails.IsListDisable")]//, attribs:" ng-disabled=gridModel.IsListDisable"
        [CustomDisplay("Leave Types")]
        public List<EmployeePromotionLeaveTypeViewModel> LeaveTypes { get; set; }

    }

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "LeaveTypes", "CRUDModel.ViewModel.PromotionLeaveDetails.LeaveTypes")]
    [DisplayName("Leave Types")]
    public class EmployeePromotionLeaveTypeViewModel : BaseMasterViewModel
    {
        public EmployeePromotionLeaveTypeViewModel()
        {
            LeaveType = new KeyValueViewModel();
        }

        public long EmployeePromotionLeaveAllocationIID { get; set; }

        public long? EmployeePromotionID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("LeaveType", "Numeric", false, "")]
        [LookUp("LookUps.LeaveType")]
        [CustomDisplay("Leave Type")]
        public KeyValueViewModel LeaveType { get; set; }
        public int? LeaveTypeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 4!")]
        [CustomDisplay("No. of days")]
        public double? AllocatedLeaves { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.PromotionLeaveDetails.LeaveTypes[0], CRUDModel.ViewModel.PromotionLeaveDetails.LeaveTypes)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.PromotionLeaveDetails.LeaveTypes[0],CRUDModel.ViewModel.PromotionLeaveDetails.LeaveTypes)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
