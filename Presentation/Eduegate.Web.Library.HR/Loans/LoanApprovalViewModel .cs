using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Loan;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.HR.Loans
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "LoanApproval", "CRUDModel.ViewModel")]
    [DisplayName("Loan/Advance Approval")]
    public class LoanApprovalViewModel : BaseMasterViewModel
    {
        public LoanApprovalViewModel()
        {
            LoanApprovalInstallments = new List<LoanApprovalInstallmentViewModel>() { new LoanApprovalInstallmentViewModel() };
            ActiveStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LOAN_ACTIVE_INSTALLMENT_STATUS");

        }

        public long? LoanRequestID { get; set; }
        public long LoanHeadIID { get; set; }
        public bool? IsApproveLoan { get; set; }
        public bool? IsLoanEntry { get; set; }
        public bool IsDisable { get; set; } = false;


        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", attribs: "ng-disabled=true")]
        [CustomDisplay("Loan/Advance Request No")]
        public string LoanRequestNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", attribs: "ng-disabled=true")]
        [CustomDisplay("Loan/Advance Request Date")]
        public string LoanRequestDateString { get; set; }
        public System.DateTime? LoanRequestDate { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", attribs: "ng-disabled=true")]
        [CustomDisplay("Loan/AdvanceNo")]
        public string LoanNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Loan/AdvanceDate")]
        public string LoanDateString { get; set; }
        public DateTime? LoanDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine10 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Employee", "String", false, "",optionalAttribute1: "ng-disabled=true")]
        [CustomDisplay("Employee")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        public KeyValueViewModel Employee { get; set; }
        public long? EmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "htmllabelinfo")]
        [CustomDisplay("Sponsorship")]
        public string SponsorShip { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown , "", attribs: "ng-disabled=true")]
        //[CustomDisplay("Designation")]
        //[LookUp("LookUps.Designation")]
        public string Designation { get; set; }
        public int? DesignationID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown , attribs: "ng-change=LoanTypeChanges($event,CRUDModel.ViewModel)")]
        [CustomDisplay("Loan/Advance Type")]
        [LookUp("LookUps.LoanTypes")]
        public string LoanType { get; set; }
        public int? LoanTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", attribs: "ng-disabled=true", Attributes2 = "ng-blur=SplitInstallAmount(CRUDModel.ViewModel)")] //, Attributes2 = "ng-blur=SplitInstallAmount(CRUDModel.ViewModel)")]
        [CustomDisplay("Loan/Advance Request  Amount")]
        public decimal? RequestLoanAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", attribs: "ng-change=CheckLoanFeasibilityAndSplitInstallAmount(CRUDModel.ViewModel)")] //, Attributes2 = "ng-blur=SplitInstallAmount(CRUDModel.ViewModel)")]  

        [CustomDisplay("Loan/Advance Approved Amount")]
        public decimal? LoanAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes = "ng-disabled='CRUDModel.ViewModel.IsDisable' ng-change='SplitInstallAmount(CRUDModel.ViewModel)'")]
        [CustomDisplay("No. Of Installments")]
        public short? NoofInstallments { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, attribs: "ng-bind=GetInstallAmount(CRUDModel.ViewModel) | number")]
        [CustomDisplay("Installment Amount")]
        public decimal? InstallmentAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", attribs: "ng-change=StartDateChange(CRUDModel.ViewModel)")]
        [CustomDisplay("Payment start date")]
        public string PaymentstartdateString { get; set; }
        public System.DateTime? Paymentstartdate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled=true")]
        [CustomDisplay("Payment End Date")]
        public string PaymentEndDateString { get; set; }

        public System.DateTime? PaymentEndDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", attribs: "ng-disabled=true")]
        [CustomDisplay("Last Installment Date")]
        public string LastInstallmentDateString { get; set; }
        public System.DateTime? LastInstallmentDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", attribs: "ng-disabled=true")]
        [CustomDisplay("Last Installment Amount")]
        public decimal? LastInstallmentAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.LoanStatus")]
        [CustomDisplay("Loan/Advance Status")]
        public string LoanStatus { get; set; }
        public byte? LoanStatusID { get; set; }

        [MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        public string ActiveStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Installments")]
        public List<LoanApprovalInstallmentViewModel> LoanApprovalInstallments { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LoanHeadDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LoanApprovalViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LoanHeadDTO, LoanApprovalViewModel>.CreateMap();
            var lrDtO = dto as LoanHeadDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<LoanHeadDTO, LoanApprovalViewModel>.Map(dto as LoanHeadDTO);

            vm.LoanRequestDateString = lrDtO.LoanRequestDate.HasValue ? lrDtO.LoanRequestDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

            vm.EmployeeID = lrDtO.EmployeeID;
            vm.LoanHeadIID = lrDtO.LoanHeadIID;
            vm.Employee = KeyValueViewModel.ToViewModel(lrDtO.Employee);
            vm.LoanRequestID = lrDtO.LoanRequestID;
            vm.LoanTypeID = lrDtO.LoanTypeID;
            vm.LoanType = lrDtO.LoanTypeID.HasValue ? lrDtO.LoanTypeID.ToString() : null;
            vm.LoanRequestNo = lrDtO.LoanRequestNo;
            vm.LoanStatus = lrDtO.LoanStatusID.HasValue ? lrDtO.LoanStatusID.ToString() : null;
            vm.NoofInstallments = lrDtO.NoOfInstallments;
            vm.LoanDate = lrDtO.LoanDate;
            vm.Paymentstartdate = lrDtO.PaymentStartDate;
            vm.PaymentstartdateString = lrDtO.PaymentStartDate.HasValue ? lrDtO.PaymentStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.PaymentEndDate = lrDtO.PaymentEndDate;
            vm.PaymentEndDateString = lrDtO.PaymentEndDate.HasValue ? lrDtO.PaymentEndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.LastInstallmentDate = lrDtO.LastInstallmentDate;
            vm.LastInstallmentDateString = lrDtO.LastInstallmentDate.HasValue ? lrDtO.LastInstallmentDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.LastInstallmentAmount = lrDtO.LastInstallmentAmount;
            vm.RequestLoanAmount = lrDtO.LoanAmount;
            vm.InstallmentAmount = lrDtO.InstallmentAmount;
            vm.Remarks = lrDtO.Remarks;
            vm.LoanDateString = lrDtO.LoanDate.HasValue ? lrDtO.LoanDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.IsDisable = lrDtO.LoanTypeID == 2 ? true : false;
            vm.SponsorShip = lrDtO.SponsorShip;

            vm.LoanApprovalInstallments = new List<LoanApprovalInstallmentViewModel>();

            foreach (var details in lrDtO.LoanDetailDTOs)
            {
                if (details != null && details.InstallmentAmount.HasValue && details.InstallmentDate.HasValue)
                {
                    vm.LoanApprovalInstallments.Add(new LoanApprovalInstallmentViewModel()
                    {
                        LoanDetailID = details.LoanDetailID,
                        LoanHeadID = details.LoanHeadID,
                        InstallmentDate = details.InstallmentDate,
                        InstallmentDateString = details.InstallmentDate.HasValue ? details.InstallmentDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        InstallmentReceivedDateString = details.InstallmentReceivedDate.HasValue ? details.InstallmentReceivedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        InstallmentReceivedDate = details.InstallmentReceivedDate,
                        InstallmentAmount = details.InstallmentAmount,
                        Remarks = details.Remarks,
                        LoanEntryStatusID = details.LoanEntryStatusID,
                        LoanEntryStatus = details.LoanEntryStatusID.HasValue ? details.LoanEntryStatusID.ToString() : null
                    });
                }
            }
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LoanApprovalViewModel, LoanHeadDTO>.CreateMap();
            var dto = Mapper<LoanApprovalViewModel, LoanHeadDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.LoanHeadIID = this.LoanHeadIID;
            dto.EmployeeID = string.IsNullOrEmpty(this.Employee.Key) ? (int?)null : int.Parse(this.Employee.Key);
            dto.LoanRequestID = this.LoanRequestID;
            dto.LoanTypeID = string.IsNullOrEmpty(this.LoanType) ? (byte?)null : byte.Parse(this.LoanType);
            dto.LoanRequestNo = this.LoanRequestNo;
            dto.LoanStatusID = string.IsNullOrEmpty(this.LoanStatus) ? (byte?)null : byte.Parse(this.LoanStatus);
            dto.NoOfInstallments = this.NoofInstallments;
            dto.LoanDate = string.IsNullOrEmpty(this.LoanDateString) ? (DateTime?)null : DateTime.ParseExact(this.LoanDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.PaymentStartDate = string.IsNullOrEmpty(this.PaymentstartdateString) ? (DateTime?)null : DateTime.ParseExact(this.PaymentstartdateString, dateFormat, CultureInfo.InvariantCulture);
            dto.LoanAmount = this.LoanAmount;
            dto.InstallmentAmount = this.InstallmentAmount;
            dto.Remarks = this.Remarks;
            foreach (var details in this.LoanApprovalInstallments)
            {
                if (details != null && details.InstallmentAmount.HasValue && details.InstallmentDate.HasValue)
                {
                    dto.LoanDetailDTOs.Add(new LoanDetailDTO()  
                    {
                        LoanDetailID = details.LoanDetailID,
                        LoanHeadID = details.LoanHeadID,
                        InstallmentReceivedDate = details.InstallmentReceivedDate,
                        InstallmentDate = string.IsNullOrEmpty(details.InstallmentDateString) ? (DateTime?)null : DateTime.ParseExact(details.InstallmentDateString, dateFormat, CultureInfo.InvariantCulture),
                        InstallmentAmount = details.InstallmentAmount,
                        Remarks = details.Remarks,
                        LoanEntryStatusID = string.IsNullOrEmpty(details.LoanEntryStatus) ? (byte?)null : byte.Parse(details.LoanEntryStatus)
                    });
                }
            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LoanHeadDTO>(jsonString);
        }

    }
}
