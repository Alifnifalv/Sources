using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels.Inventory;

namespace Eduegate.Web.Library.ViewModels.Warehouses
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "JobEntryMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Job Entry")]
    public class JobEntryMasterViewModel : BaseMasterViewModel
    {
        public JobEntryMasterViewModel()
        {
            DeliveryDetails = new DeliveryAddressViewModel();
            Branch = new KeyValueViewModel();
            DocumentType = new KeyValueViewModel();
        }

        public long JobEntryHeadIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Branch", "String", false, "BranchChange($event, $element)", false)]
        [DisplayName("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public KeyValueViewModel Branch { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("DocumentType", "String", false, "DocumentTypeChange($event, $element)")]
        [DisplayName("Document")]
        [LookUp("LookUps.DocumentType")]
        //[HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Job No")]
        public string TransactionNo { get; set; }

        public string JobNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Remarks")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Remarks { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date")]
        public String JobStartDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Due Date")]
        public string JobEndDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Assinged")]
        [Select2("Employee", "Numeric", false)]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        public KeyValueViewModel Employee { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("Select transaction")]
        [DataPicker("Transaction")]
        public String ReferenceTransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Job Status")]
        [LookUp("LookUps.JobStatus")]
        public string JobStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Priority")]
        [LookUp("LookUps.JobPriority")]
        public string JobPriority { get; set; }

        public string JobOperationStatus { get; set; }

        public string Basket { get; set; }

        public string ProcessStartDate { get; set; }

        public string ProcessEndDate { get; set; }

        public string SearchText { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliveryDetails", "CRUDModel.Model.MasterViewModel.DeliveryDetails")]
        [DisplayName("Delivery Details")]
        public DeliveryAddressViewModel DeliveryDetails { get; set; }

        public int JobStatusId { get; set; }
        public byte JobOperationStatusId { get; set; }

    }
}
