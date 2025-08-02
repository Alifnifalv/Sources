using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;
using System;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "LoanApprovalInstallments", "CRUDModel.ViewModel.LoanApprovalInstallments")]
    [DisplayName("Installments")]
    public class LoanApprovalInstallmentViewModel : BaseMasterViewModel
    {
        public LoanApprovalInstallmentViewModel()
        {
            
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("InstallmentDate")]

        public string InstallmentDateString { get; set; }
        public System.DateTime? InstallmentDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-bind=GetInstallAmount(CRUDModel.ViewModel) | number")]
        [CustomDisplay("Installment Amount")]
        public decimal? InstallmentAmount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown,"", Attributes2= "ng-change='LoanEntryStatusChanges(gridModel)' ng-disabled=gridModel.IsDisableStatus")]
        [CustomDisplay("Status")]
        [LookUp("LookUps.LoanEntryStatus")]
        public string LoanEntryStatus { get; set; }
        public byte? LoanEntryStatusID { get; set; }

        [MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", attribs: "ng-disabled=true")]
        [CustomDisplay("Installment Recieved Date")]
        public string InstallmentReceivedDateString { get; set; }

        public System.DateTime? InstallmentReceivedDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]//, Attributes = "ng-disabled=true")]
        [CustomDisplay("Paid Amount")]
        public decimal? PaidAmount { get; set; }

        public long LoanDetailID { get; set; }

        public bool? IsDisableStatus { get; set; }
        public long? LoanHeadID { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.LoanApprovalInstallments[0], CRUDModel.ViewModel.LoanApprovalInstallments)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.LoanApprovalInstallments[0],CRUDModel.ViewModel.LoanApprovalInstallments)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}