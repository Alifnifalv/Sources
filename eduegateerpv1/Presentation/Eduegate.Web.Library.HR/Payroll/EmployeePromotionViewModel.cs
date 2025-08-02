using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Payroll;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmployeePromotion", "CRUDModel.ViewModel")]
    [DisplayName("Employee Promotion Details")]
    public class EmployeePromotionViewModel : BaseMasterViewModel
    {
        public EmployeePromotionViewModel()
        {
            SalaryComponents = new List<EmployeePromotionSalaryComponentViewModel>() { new EmployeePromotionSalaryComponentViewModel() };
            PromotionLeaveDetails = new EmployeePromotionLeaveDetailsViewModel();
            TimeSheetSetting = new SalaryTimeSheetSettingViewModel();
            EmployeeSalaryStructure = new KeyValueViewModel();
            PayrollFrequency = new KeyValueViewModel();
            OldDesignation = new KeyValueViewModel();
            NewDesignation = new KeyValueViewModel();
            PaymentMode = new KeyValueViewModel();
            Employee = new KeyValueViewModel();
            OldBranch = new KeyValueViewModel();
            NewBranch = new KeyValueViewModel();
            Account = new KeyValueViewModel();
            IsApplyImmediately = false;
        }

        public long EmployeePromotionIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Employee", "Numeric", false, "EmployeeChanges($event, $element, CRUDModel.ViewModel)")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=FullEmployee", "LookUps.FullEmployee")]
        [CustomDisplay("EmployeeName")]
        public KeyValueViewModel Employee { get; set; }
        public long? EmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Branch", "String", false, "")]
        [LookUp("LookUps.Branch")]
        [CustomDisplay("Current Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public KeyValueViewModel OldBranch { get; set; }

        public Nullable<long> OldBranchID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Branch", "Numeric", false, "")]
        [LookUp("LookUps.Branch")]
        [CustomDisplay("Promoted Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public KeyValueViewModel NewBranch { get; set; }

        public Nullable<long> NewBranchID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Designation", "Numeric", false, "")]
        [LookUp("LookUps.Designation")]
        [CustomDisplay("Current Designation")]
        public KeyValueViewModel OldDesignation { get; set; }

        public Nullable<int> OldDesignationID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Designation", "Numeric", false, "")]
        [LookUp("LookUps.Designation")]
        [CustomDisplay("Promoted Designation")]
        public KeyValueViewModel NewDesignation { get; set; }

        public Nullable<int> NewDesignationID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        //[Required]


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }



        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SalaryStructure", "Numeric", false, "SalaryStructureChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.SalaryStructure")]
        [CustomDisplay("SalaryStructureName")]
        public KeyValueViewModel EmployeeSalaryStructure { get; set; }
        public long? SalaryStructureID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("FromDate")]
        public string FromdateString { get; set; }
        public System.DateTime? Fromdate { get; set; }



        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("PaymentMode", "Numeric", false, "")]
        [LookUp("LookUps.PaymentModes")]
        [CustomDisplay("PaymentMode")]
        public KeyValueViewModel PaymentMode { get; set; }
        public int? PaymentModeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("PayrollFrequency", "Numeric", false, "")]
        [LookUp("LookUps.PayrollFrequency")]
        [CustomDisplay("PayrollFrequency")]
        public KeyValueViewModel PayrollFrequency { get; set; }
        public byte? PayrollFrequencyID { get; set; }



        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, Attributes = "ng-bind=GetTotalAmount(CRUDModel.ViewModel.SalaryComponents) | number")]
        //[CustomDisplay("TotalAmount")]
        public decimal? Amount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SalaryAccount", "Numeric", false, "")]
        [LookUp("LookUps.SalaryAccount")]
        [CustomDisplay("Account")]
        public KeyValueViewModel Account { get; set; }
        public int? AccountID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Apply Immediately")]
        public bool? IsApplyImmediately { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "PromotionLeaveDetails", "PromotionLeaveDetails")]
        [CustomDisplay("Employee Leave Details")]
        public EmployeePromotionLeaveDetailsViewModel PromotionLeaveDetails { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Components")]
        public List<EmployeePromotionSalaryComponentViewModel> SalaryComponents { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmpTimeSheetSetting", "EmpTimeSheetSetting")]
        [CustomDisplay("TimesheetSetting")]
        public SalaryTimeSheetSettingViewModel TimeSheetSetting { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EmployeePromotionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeePromotionViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EmployeePromotionDTO, EmployeePromotionViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<EmployeePromotionComponentMapDTO, EmployeePromotionSalaryComponentViewModel>.CreateMap();
            Mapper<EmployeePromotionLeaveAllocDTO, EmployeePromotionLeaveTypeViewModel>.CreateMap();
            Mapper<EmployeePromotionDTO, SalaryTimeSheetSettingViewModel>.CreateMap();

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var ssDto = dto as EmployeePromotionDTO;
            var vm = Mapper<EmployeePromotionDTO, EmployeePromotionViewModel>.Map(ssDto);
            //vm.Fromdate = ssDto.FromDate;
            vm.FromdateString = (ssDto.FromDate.HasValue ? ssDto.FromDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.SalaryComponents = new List<EmployeePromotionSalaryComponentViewModel>();
            vm.Employee = KeyValueViewModel.ToViewModel(ssDto.Employee);
            vm.OldDesignation = KeyValueViewModel.ToViewModel(ssDto.OldDesignation);
            vm.OldBranch = KeyValueViewModel.ToViewModel(ssDto.OldBranch);
            vm.PromotionLeaveDetails.OldLeaveGroup = KeyValueViewModel.ToViewModel(ssDto.OldLeaveGroup);
            vm.NewDesignation = KeyValueViewModel.ToViewModel(ssDto.NewDesignation);
            vm.NewBranch = KeyValueViewModel.ToViewModel(ssDto.NewBranch);
            vm.PromotionLeaveDetails.NewLeaveGroup = KeyValueViewModel.ToViewModel(ssDto.NewLeaveGroup);
            vm.OldDesignationID = ssDto.OldDesignationID;
            vm.OldBranchID = ssDto.OldBranchID;
            vm.PromotionLeaveDetails.OldLeaveGroupID = ssDto.OldLeaveGroupID;

            vm.NewDesignationID = ssDto.NewDesignationID;
            vm.NewBranchID = ssDto.NewBranchID;
            vm.PromotionLeaveDetails.NewLeaveGroupID = ssDto.NewLeaveGroupID;
            vm.IsApplyImmediately = ssDto.IsApplyImmediately;
            foreach (var salaryComp in ssDto.SalaryComponents)
            {

                vm.SalaryComponents.Add(new EmployeePromotionSalaryComponentViewModel()
                {
                    EmployeePromotionSalaryComponentMapIID = salaryComp.EmployeePromotionSalaryComponentMapIID,
                    EmployeePromotionID = salaryComp.EmployeePromotionID,
                    SalaryComponentID = string.IsNullOrEmpty(salaryComp.SalaryComponent.Key) ? (int?)null : int.Parse(salaryComp.SalaryComponent.Key),
                    SalaryComponent = KeyValueViewModel.ToViewModel(salaryComp.SalaryComponent),
                    Deduction = salaryComp.Amount.Value < 0 ? salaryComp.Amount.Value * -1 : (decimal?)null,
                    Earnings = salaryComp.Amount.Value > 0 ? salaryComp.Amount.Value * 1 : (decimal?)null,
                });
            }
            vm.PromotionLeaveDetails.LeaveTypes = new List<EmployeePromotionLeaveTypeViewModel>();
            //vm.PromotionLeaveDetails.OldLeaveGroup=
            foreach (var leaveTypes in ssDto.EmployeePromotionLeaveAllocs)
            {
                vm.PromotionLeaveDetails.LeaveTypes.Add(new EmployeePromotionLeaveTypeViewModel()
                {
                    EmployeePromotionLeaveAllocationIID = leaveTypes.EmployeePromotionLeaveAllocationIID,
                    EmployeePromotionID = leaveTypes.EmployeePromotionID,
                    AllocatedLeaves = leaveTypes.AllocatedLeaves,
                    LeaveTypeID = leaveTypes.LeaveTypeID,
                    LeaveType = KeyValueViewModel.ToViewModel(leaveTypes.LeaveType),
                });
            }
            vm.EmployeeSalaryStructure = new KeyValueViewModel()
            {
                Key = ssDto.EmployeeSalaryStructure.Key.ToString(),
                Value = ssDto.EmployeeSalaryStructure.Value
            };
            vm.TimeSheetSetting.TimeSheetSalaryComponentID = ssDto.TimeSheetSalaryComponentID;
            vm.TimeSheetSetting.TimeSheetSalaryComponent = ssDto.TimeSheetSalaryComponentID.HasValue ? new KeyValueViewModel()
            {
                Key = ssDto.TimeSheetSalaryComponentID.ToString(),
                Value = ssDto.TimeSheetSalaryComponent.Value
            } : null;
            vm.TimeSheetSetting.TimeSheetMaximumBenefits = ssDto.TimeSheetMaximumBenefits;
            vm.TimeSheetSetting.IsSalaryBasedOnTimeSheet = ssDto.IsSalaryBasedOnTimeSheet;
            vm.TimeSheetSetting.TimeSheetLeaveEncashmentPerDay = ssDto.TimeSheetLeaveEncashmentPerDay;
            vm.TimeSheetSetting.TimeSheetHourRate = ssDto.TimeSheetHourRate;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EmployeePromotionViewModel, EmployeePromotionDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<EmployeePromotionLeaveTypeViewModel, EmployeePromotionLeaveAllocDTO>.CreateMap();
            Mapper<SalaryTimeSheetSettingViewModel, EmployeePromotionDTO>.CreateMap();
            Mapper<EmployeePromotionSalaryComponentViewModel, EmployeePromotionComponentMapDTO>.CreateMap();

            var dto = Mapper<EmployeePromotionViewModel, EmployeePromotionDTO>.Map(this);
            dto.EmployeePromotionIID = this.EmployeePromotionIID;
            //dto.FromDate = this.Fromdate;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.FromDate = string.IsNullOrEmpty(this.FromdateString) ? (DateTime?)null : DateTime.ParseExact(this.FromdateString, dateFormat, CultureInfo.InvariantCulture);

            dto.SalaryStructureID = string.IsNullOrEmpty(this.EmployeeSalaryStructure.Key) ? (long?)null : long.Parse(this.EmployeeSalaryStructure.Key);

            dto.PayrollFrequencyID = string.IsNullOrEmpty(this.PayrollFrequency.Key) ? (byte?)null : byte.Parse(this.PayrollFrequency.Key);

            dto.PaymentModeID = string.IsNullOrEmpty(this.PaymentMode.Key) ? (byte?)null : byte.Parse(this.PaymentMode.Key);
            dto.AccountID = this.Account == null || string.IsNullOrEmpty(this.Account.Key) ? (byte?)null : byte.Parse(this.Account.Key);
            dto.SalaryComponents = new List<EmployeePromotionComponentMapDTO>();
            dto.IsApplyImmediately = this.IsApplyImmediately;
            dto.EmployeeID = string.IsNullOrEmpty(this.Employee.Key) ? (long?)null : long.Parse(this.Employee.Key);
            dto.Employee = new KeyValueDTO { Key = this.Employee.Key, Value = this.Employee.Value };
            dto.OldDesignationID = string.IsNullOrEmpty(this.OldDesignation.Key) ? (int?)null : int.Parse(this.OldDesignation.Key);
            dto.OldBranchID = string.IsNullOrEmpty(this.OldBranch.Key) ? (int?)null : int.Parse(this.OldBranch.Key);
            dto.OldLeaveGroupID = this.PromotionLeaveDetails.OldLeaveGroup == null || string.IsNullOrEmpty(this.PromotionLeaveDetails.OldLeaveGroup.Key) ? (int?)null : int.Parse(this.PromotionLeaveDetails.OldLeaveGroup.Key);

            dto.NewDesignationID = string.IsNullOrEmpty(this.NewDesignation.Key) ? (int?)null : int.Parse(this.NewDesignation.Key);
            dto.NewBranchID = string.IsNullOrEmpty(this.NewBranch.Key) ? (int?)null : int.Parse(this.NewBranch.Key);
            dto.NewLeaveGroupID = this.PromotionLeaveDetails.NewLeaveGroup == null || string.IsNullOrEmpty(this.PromotionLeaveDetails.NewLeaveGroup.Key) ? (int?)null : int.Parse(this.PromotionLeaveDetails.NewLeaveGroup.Key);

            foreach (var salaryComp in this.SalaryComponents)
            {
                if (salaryComp.SalaryComponent != null && !string.IsNullOrEmpty(salaryComp.SalaryComponent.Key))
                {
                    dto.SalaryComponents.Add(new EmployeePromotionComponentMapDTO()
                    {
                        EmployeePromotionSalaryComponentMapIID = salaryComp.EmployeePromotionSalaryComponentMapIID,
                        EmployeePromotionID = salaryComp.EmployeePromotionID,
                        SalaryComponentID = string.IsNullOrEmpty(salaryComp.SalaryComponent.Key) ? (int?)null : int.Parse(salaryComp.SalaryComponent.Key),
                        Amount = salaryComp.Earnings.HasValue ? salaryComp.Earnings : salaryComp.Deduction.HasValue ? salaryComp.Deduction * -1 : (decimal?)null,

                    });
                }
            }
            dto.EmployeePromotionLeaveAllocs = new List<EmployeePromotionLeaveAllocDTO>();

            foreach (var leaveTypes in this.PromotionLeaveDetails.LeaveTypes)
            {
                if (leaveTypes.LeaveType != null && !string.IsNullOrEmpty(leaveTypes.LeaveType.Key))
                {
                    dto.EmployeePromotionLeaveAllocs.Add(new EmployeePromotionLeaveAllocDTO()
                    {
                        EmployeePromotionLeaveAllocationIID = leaveTypes.EmployeePromotionLeaveAllocationIID,
                        EmployeePromotionID = leaveTypes.EmployeePromotionID,
                        AllocatedLeaves = leaveTypes.AllocatedLeaves,
                        LeaveTypeID = string.IsNullOrEmpty(leaveTypes.LeaveType.Key) ? (int?)null : int.Parse(leaveTypes.LeaveType.Key),

                    });
                }
            }
            dto.Amount = dto.SalaryComponents.Sum(x => (x.Amount));
            //dto.IsActive = string.IsNullOrEmpty(this.IsActive);
            dto.TimeSheetMaximumBenefits = this.TimeSheetSetting.TimeSheetMaximumBenefits;
            dto.IsSalaryBasedOnTimeSheet = this.TimeSheetSetting.IsSalaryBasedOnTimeSheet;
            dto.TimeSheetLeaveEncashmentPerDay = this.TimeSheetSetting.TimeSheetLeaveEncashmentPerDay;
            dto.TimeSheetHourRate = this.TimeSheetSetting.TimeSheetHourRate;
            //dto.TimeSheetSalaryComponentID = this.TimeSheetSetting.TimeSheetSalaryComponent == null || string.IsNullOrEmpty(this.TimeSheetSetting.TimeSheetSalaryComponent.Key.ToString()) ? (int?)null : int.Parse(this.TimeSheetSetting.TimeSheetSalaryComponent.Key);
            dto.TimeSheetSalaryComponentID = this.TimeSheetSetting.TimeSheetSalaryComponent == null ||
            string.IsNullOrEmpty(this.TimeSheetSetting.TimeSheetSalaryComponent.Key) ? (int?)null : int.Parse(this.TimeSheetSetting.TimeSheetSalaryComponent.Key);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeePromotionDTO>(jsonString);
        }

    }
}