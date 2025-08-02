using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Enums;

namespace Eduegate.Web.Library.ViewModels.Warehouses
{
    [Order(1)]
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "JobOperationHead", "Model.MasterViewModel")]
    [DisplayName("Job Details")]
    public class JobOperationHeadViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Branch")]
        [LookUp("LookUps.Branch")]
        [Order(1)]
        public string Branch { get; set; }
        public long BranchID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Document")]
        [Order(2)]
        public string DocumentType { get; set; }
        public long DocumentTypeID { get; set; }
        public long LoginID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Job ID")]
        [Order(3)]
        public long TransactionHeadIID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Job Number")]
        [Order(4)]
        public string JobNumber { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Date")]
        [Order(5)]
        public string JobDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Due Date")]
        [Order(6)]
        public string DueDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Remarks")]
        [Order(7)]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Transaction")]
        [Order(8)]
        public string ReferenceTransaction { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Priority")]
        [Order(9)]
        public string JobPriority { get; set; }

        public int? JobPriorityID { get; set; }

        public Nullable<long> EmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Employee Name")]
        [Order(10)]
        public string EmployeeName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Notes")]
        //[Order(11)]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "hours-remaining", "ng-bind=\"ShowRemainingHours(Model.MasterViewModel.DueDate, Model.MasterViewModel.JobOperationStatus.Key)\"")]
        [DisplayName("Hours Remaining")]
        [Order(12)]
        public string HoursRemaining { get; set; }

        [ControlType(Framework.Enums.ControlTypes.YesNoCheckBox, "", "ng-change='PickJob($event, $element)' ng-disabled='Model.IsLoginUserJob ?  true: false'")]
        [DisplayName("Pick the job")]
        [Order(13)]
        public bool IsPicked { get; set; }

        public JobOperationTypes OperationTypes { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Assinged Back")]
        [Select2("Employee", "Numeric", false)]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        [Order(14)]
        public KeyValueViewModel AssignBackEmployee { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Operation Status")]
        [Select2("JobOperationStatus", "Numeric", false)]
        [LookUp("LookUps.JobOperationStatus")]
        [Order(15)]
        public KeyValueViewModel JobOperationStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "hours-remaining", "ng-bind=\"ShowHoursTakenForJob(Model.MasterViewModel.DueDate, Model.MasterViewModel.JobOperationStatus.Key)\"")]
        [DisplayName("Hours Taken")]
        [Order(16)]
        public string HoursTaken { get; set; }

        public String ReferenceTransactionNo { get; set; }

        public Nullable<int> JobStatusID { get; set; }

        //public string Basket { get; set; }

        public string ProcessStartDate { get; set; }

        public string ProcessEndDate { get; set; }

        public bool IsDoneFlag { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Job Size")]
        [LookUp("LookUps.JobSize")]
        [Order(17)]
        public string JobSize { get; set; }
        public Nullable<long> ParentJobEntryHeadId { get; set; }
    }
}
