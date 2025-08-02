using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.ViewModels.Inventory.Purchase
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "PurchaseRequestMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Purchase Request")]
    public class PurchaseRequestMasterViewModel : BaseMasterViewModel
    {
        public PurchaseRequestMasterViewModel()
        {
            DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.PurchaseRequest;
            Approver = new KeyValueViewModel();
            Requisitioned = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            Branch = new KeyValueViewModel();
        }
        public long TransactionHeadIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public KeyValueViewModel DocumentStatus { get; set; }
        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")] //ng-disabled = 'CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != null'
        [Select2("Branch", "String", false, "BranchChange($event, $element)", false, optionalAttribute1: "ng-disabled='CRUDModel.Model.MasterViewModel.TransactionHeadIID !=0'")]
        [CustomDisplay("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public KeyValueViewModel Branch { get; set; }
        public long BranchID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("DocumentType", "String", false, "DocumentTypeChange($event, $element)", optionalAttribute1: "ng-disabled=true")]
        [DisplayName("Document")]
        [LookUp("LookUps.DocumentType")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "alignleft")]
        [CustomDisplay("Req.No")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Date")]
        public string TransactionDate { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Qty needed")]
        public string Quantity { get; set; }

        public decimal TotalCost { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Requisitioned by")]
        [Select2("RequisitionedID", "Numeric", false, "GetEmployeeDetailsByEmployeeID($event, $element); OnChangeSelect2", false)]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        public KeyValueViewModel Requisitioned { get; set; }
        public long? RequestedByID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Department")]
        public string RequisitionedDepartment { get; set; }
        public long? DepartmentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Approved by")]
        [Select2("RequisitionedID", "Numeric", false, "", false)]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        public KeyValueViewModel Approver { get; set; }
        public long? ApproverID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }
    }
}