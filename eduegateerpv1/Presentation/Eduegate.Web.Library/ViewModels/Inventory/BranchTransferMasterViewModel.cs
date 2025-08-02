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
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "BranchTransferMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Branch Transfer Details")]
    public class BranchTransferMasterViewModel : BaseMasterViewModel
    {
        public BranchTransferMasterViewModel()
        {
            DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.BranchTransfer;
            DocumentStatus = new KeyValueViewModel();
        }

        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [CustomDisplay("BTRNumber")]
        [DataPicker("BranchTransferRequest")]
        public String ReferenceTransactionNo { get; set; }
        public String ReferenceTransactionHeaderID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Branch", "String", false, "BranchChange($event, $element)", false)]
        [CustomDisplay("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public KeyValueViewModel Branch { get; set; }

        public long BranchID { get; set; }

        public long TransactionHeadIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("DocumentType", "String", false, "DocumentTypeChange($event, $element)")]
        [CustomDisplay("Document")]
        [LookUp("LookUps.DocumentType")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }

        public long DocumentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [CustomDisplay("DocumentStatus")]
        public KeyValueViewModel DocumentStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("BTNo")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Date")]
        public String TransactionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Shipment")]
        public bool IsShipment { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("ToBranch")]
        [LookUp("LookUps.Branch")]
        public string ToBranch { get; set; }

        public long ToBranchID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Reference#")]
        [MaxLength(25, ErrorMessage = "Maximum Length should be within 25!")]
        public string Reference { get; set; }

        private int documentReferenceTypeID;
        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }

        public bool IsTransactionCompleted { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}
