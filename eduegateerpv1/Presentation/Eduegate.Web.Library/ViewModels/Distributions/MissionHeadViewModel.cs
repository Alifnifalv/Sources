using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Warehouses;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MissionHead", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Mission Details")]
    public class MissionHeadViewModel : BaseMasterViewModel
    { 
        public MissionHeadViewModel()
        {
            IsServiceProver = false;
        }

        public long JobEntryHeadIID { get; set; }

        public long BranchID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown, "onecol-header-left")]
        //[DisplayName("Branch")]
        //[LookUp("LookUps.Branch")]
        public string Branch { get; set; }

        public long DocumentTypeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "onecol-header-left", "ng-change='DocumentTypeChange($event, $element)'")]
        [DisplayName("Document")]
        [LookUp("LookUps.DocumentType")]
        public string DocumentType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Job No")]
        public string TransactionNo { get; set; }

        public string JobNumber { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Remarks")]
        public string Remarks { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date")]
        public String JobStartDate { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DatePicker)]
        //[DisplayName("Due Date")]
        public string JobEndDate { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Service Provider")]
        [Select2("ServiceProvider", "Numeric", false, "OnServiceProviderChange($event, CRUDModel.Model.MasterViewModel)")]
        [LookUp("LookUps.ServiceProvider")]
        public KeyValueViewModel ServiceProvider { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("")]
        public string BlankLine { get; set; }

        public bool IsServiceProver { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Driver")]
        [Select2("Driver", "Numeric", false, null, false, " ng-disabled= CRUDModel.Model.MasterViewModel.IsServiceProver ")]
        //[LookUp("LookUps.Driver")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Driver", "LookUps.Driver")]
        public KeyValueViewModel Driver { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Vehicle")]
        [Select2("Vehicle", "Numeric", false, null, false, " ng-disabled= CRUDModel.Model.MasterViewModel.IsServiceProver ")]
        [LookUp("LookUps.Vehicle")]
        public KeyValueViewModel Vehicle { get; set; }    

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Job Status")]
        public string JobStatus { get; set; }

        public int JobStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Operation Status")]
        [Order(15)]
        public string JobOperationStatus { get; set; }

        public byte? JobOperationStatusID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Priority")]
        //[LookUp("LookUps.JobPriority")]
        public string JobPriority { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("Add Job")]
        [DataPicker("ReadyForShipping")]
        public String AddJobNo { get; set; }
    }
}
