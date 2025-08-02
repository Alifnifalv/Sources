using Eduegate.Domain.Entity.HR.Loan;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Services.Contracts.HR.Loan;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Eduegate.Web.Library.HR.Loans
{
    public class LoanRequestViewModel : BaseMasterViewModel
    {
        public long LoanRequestIID { get; set; }
        public bool IsDisable { get; set; } = false;
        public bool IsAllDisable { get; set; } = true;

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Loan/AdvanceRequest No.")]
        public string LoanRequestNo { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Loan/AdvanceRequest Date")]
        public string LoanRequestDateString { get; set; }
        public System.DateTime? LoanRequestDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Employee", "String", false, "")]
        [CustomDisplay("Employee")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=NonSettledEmployees", "LookUps.NonSettledEmployees")]
        public KeyValueViewModel Employee { get; set; }
        public long? EmployeeID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-disabled=true")]
        //[CustomDisplay("Designation")]
        //[LookUp("LookUps.Designation")]
        //public string Designation { get; set; }
        public int? DesignationID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "medium-col-width", attribs: "ng-change=LoanTypeChanges($event,CRUDModel.ViewModel)")]
        [CustomDisplay("Loan/AdvanceType")]
        [LookUp("LookUps.LoanTypes")]
        public string LoanType { get; set; }
        public byte? LoanTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox,"textright", "ng-blur='CheckLoanFeasibility(CRUDModel.ViewModel)'" , Attributes = "ng-disabled='CRUDModel.ViewModel.IsAllDisable'")]
        [CustomDisplay("Loan/AdvanceAmount")]
        public decimal? LoanAmount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "textright",  attribs: "ng-disabled=CRUDModel.ViewModel.IsDisable", Attributes3 = "ng-blur='CheckLoanFeasibility(CRUDModel.ViewModel)'")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-disabled='CRUDModel.Model.MasterViewModel.RecruitmentType.Key != 2' ng-blur='ValidateField($event,$element,\"CIVILID\",\"HR/EmploymentRequest\")'")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes = "ng-disabled='CRUDModel.ViewModel.IsDisable' ng-Click='CheckLoanFeasibility(CRUDModel.ViewModel)'")]
        [CustomDisplay("NoOfInstallments")]
        public short? NoofInstallments { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("PaymentStartDate")]
        public string PaymentstartdateString { get; set; }
        public System.DateTime? Paymentstartdate { get; set; }

       
        [ControlType(Framework.Enums.ControlTypes.Label,attribs: "ng-bind=GetInstallAmount(CRUDModel.ViewModel) | number")]
        [CustomDisplay("InstallmentAmount")]
        public decimal? InstallmentAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-disabled = true")]
        [LookUp("LookUps.LoanRequestStatus")]
        [CustomDisplay("Loan/AdvanceRequestStatus")]
        public string LoanRequestStatus { get; set; } = "1";
        public byte? LoanRequestStatusID { get; set; }
      

        [MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LoanRequestDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LoanRequestViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LoanRequestDTO, LoanRequestViewModel>.CreateMap();
            var lrDtO = dto as LoanRequestDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<LoanRequestDTO, LoanRequestViewModel>.Map(dto as LoanRequestDTO);
            vm.EmployeeID = lrDtO.EmployeeID;
            vm.Employee = KeyValueViewModel.ToViewModel(lrDtO.Employee);
            vm.LoanRequestIID = lrDtO.LoanRequestIID;
            vm.LoanTypeID = lrDtO.LoanTypeID;
            vm.LoanType = lrDtO.LoanTypeID.HasValue ? lrDtO.LoanTypeID.ToString() : null;
            vm.LoanRequestNo = lrDtO.LoanRequestNo;
            vm.LoanRequestStatusID = lrDtO.LoanRequestStatusID;
            vm.LoanRequestStatus = lrDtO.LoanRequestStatusID.HasValue ? lrDtO.LoanRequestStatusID.ToString() : null;
            vm.NoofInstallments = lrDtO.NoOfInstallments;
            vm.LoanRequestDate = lrDtO.LoanRequestDate;
            vm.PaymentstartdateString = lrDtO.PaymentStartDate.HasValue ? lrDtO.PaymentStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.LoanAmount = lrDtO.LoanAmount;
            vm.InstallmentAmount = lrDtO.InstallmentAmount;
            vm.Remarks = lrDtO.Remarks;
            vm.LoanRequestDateString = lrDtO.LoanRequestDate.HasValue ? lrDtO.LoanRequestDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.IsDisable = lrDtO.LoanTypeID == 2 ? true : false; 
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LoanRequestViewModel, LoanRequestDTO>.CreateMap();
            var dto = Mapper<LoanRequestViewModel, LoanRequestDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.EmployeeID = string.IsNullOrEmpty(this.Employee.Key) ? (long?)null : long.Parse(this.Employee.Key);            
            dto.LoanRequestIID = this.LoanRequestIID;           
            dto.LoanRequestNo = this.LoanRequestNo;            
            dto.NoOfInstallments = this.NoofInstallments;
            dto.LoanRequestDate = this.LoanRequestDate;
            dto.LoanAmount = this.LoanAmount;
            dto.InstallmentAmount = this.InstallmentAmount;
            dto.Remarks = this.Remarks;
            dto.LoanTypeID = string.IsNullOrEmpty(this.LoanType) ? (byte?)null : byte.Parse(this.LoanType);
            dto.LoanRequestStatusID = string.IsNullOrEmpty(this.LoanRequestStatus) ? (byte?)null : byte.Parse(this.LoanRequestStatus);
            dto.LoanRequestDate = string.IsNullOrEmpty(this.LoanRequestDateString) ? (DateTime?)null : DateTime.ParseExact(this.LoanRequestDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.PaymentStartDate = string.IsNullOrEmpty(this.PaymentstartdateString) ? (DateTime?)null : DateTime.ParseExact(this.PaymentstartdateString, dateFormat, CultureInfo.InvariantCulture);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LoanRequestDTO>(jsonString);
        }

    }
}
