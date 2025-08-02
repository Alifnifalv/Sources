using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmployeeSalaryStructure", "CRUDModel.ViewModel")]
    [DisplayName("Employee Salary Structure")]
    public class EmployeeSalaryStructureViewModel : BaseMasterViewModel
    {

        public EmployeeSalaryStructureViewModel()
        {
            TimeSheetSetting = new SalaryTimeSheetSettingViewModel();
            Employee = new List<KeyValueViewModel>();
            MasterSalaryComponents = new EmployeeSalaryComponentsViewModel();
            MasterLeaveSalaryComponents = new EmployeeLeaveSalaryComponentsViewModel();
            IsActive = false;

            SalaryComponents = new List<EmployeeSalaryStructureComponentViewModel>() { new EmployeeSalaryStructureComponentViewModel() };
            LeaveSalaryComponents = new List<EmployeeLeaveSalaryStructureComponentViewModel>() { new EmployeeLeaveSalaryStructureComponentViewModel() };
        }

        public long EmployeeSalaryStructureIID { get; set; }

        //public bool? IsActive { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Employee", "Numeric", true, "")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=FullEmployee", "LookUps.FullEmployee")]
        [CustomDisplay("EmployeeName")]
        public List<KeyValueViewModel> Employee { get; set; }
        public long? EmployeeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("FromDate")]
        public string FromdateString { get; set; }
        public System.DateTime? Fromdate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("PayrollFrequency", "Numeric", false, "")]
        [LookUp("LookUps.PayrollFrequency")]
        [CustomDisplay("PayrollFrequency")]
        public KeyValueViewModel PayrollFrequency { get; set; }
        public byte? PayrollFrequencyID { get; set; }


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
        [Select2("SalaryAccount", "Numeric", false, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=SalaryAccount", "LookUps.SalaryAccount")]
        //[LookUp("LookUps.SalaryAccount")]
        [CustomDisplay("Account")]
        public KeyValueViewModel SalaryAccount { get; set; }
        public int? AccountID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, Attributes = "ng-bind=GetTotalAmount(CRUDModel.ViewModel.MasterSalaryComponents.SalaryComponents) | number")]
        [CustomDisplay("TotalAmount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "SalaryComponents", "SalaryComponents")]
        [CustomDisplay("Components")]
        public EmployeeSalaryComponentsViewModel MasterSalaryComponents { get; set; }
        public List<EmployeeSalaryStructureComponentViewModel> SalaryComponents { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "LeaveSalaryComponents", "LeaveSalaryComponents")]
        [CustomDisplay("Leave Salary / Vacation Salary Components")]
        public EmployeeLeaveSalaryComponentsViewModel MasterLeaveSalaryComponents { get; set; }
        public List<EmployeeLeaveSalaryStructureComponentViewModel> LeaveSalaryComponents { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmpTimeSheetSetting", "EmpTimeSheetSetting")]
        [CustomDisplay("TimesheetSetting")]
        public SalaryTimeSheetSettingViewModel TimeSheetSetting { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EmployeeSalaryStructureDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeSalaryStructureViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EmployeeSalaryStructureDTO, EmployeeSalaryStructureViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            //Mapper<EmployeeSalaryStructureComponentMapDTO, EmployeeSalaryStructureComponentViewModel>.CreateMap();
            Mapper<EmployeeSalaryStructureDTO, SalaryTimeSheetSettingViewModel>.CreateMap();
            Mapper<EmployeeSalaryStructureDTO, EmployeeSalaryComponentsViewModel>.CreateMap();
            Mapper<EmployeeSalaryStructureDTO, EmployeeLeaveSalaryComponentsViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var ssDto = dto as EmployeeSalaryStructureDTO;
            var vm = Mapper<EmployeeSalaryStructureDTO, EmployeeSalaryStructureViewModel>.Map(ssDto);
            //vm.Fromdate = ssDto.FromDate;
            vm.FromdateString = (ssDto.FromDate.HasValue ? ssDto.FromDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.MasterSalaryComponents.SalaryComponents = new List<EmployeeSalaryStructureComponentViewModel>();
            vm.MasterLeaveSalaryComponents.LeaveSalaryComponents = new List<EmployeeLeaveSalaryStructureComponentViewModel>();
            //vm.SalaryAccount = ssDto.AccountID.HasValue ? ssDto.AccountID : (long?)null;
            vm.SalaryAccount = ssDto.AccountID.HasValue ? new KeyValueViewModel() { Key = ssDto.Account.Key, Value = ssDto.Account.Value } : new KeyValueViewModel();

            vm.IsActive = ssDto.IsActive;
            vm.Amount = ssDto.Amount;

            foreach (var salaryComp in ssDto.SalaryComponents)
            {
                if (salaryComp != null)
                {
                    vm.MasterSalaryComponents.SalaryComponents.Add(new EmployeeSalaryStructureComponentViewModel()
                    {
                        EmployeeSalaryStructureComponentMapIID = salaryComp.EmployeeSalaryStructureComponentMapIID,
                        EmployeeSalaryStructureID = salaryComp.EmployeeSalaryStructureID,
                        SalaryComponentID = string.IsNullOrEmpty(salaryComp.SalaryComponent.Key) ? (int?)null : int.Parse(salaryComp.SalaryComponent.Key),
                        SalaryComponent = KeyValueViewModel.ToViewModel(salaryComp.SalaryComponent),
                        //Amount = salaryComp.Amount.HasValue ? salaryComp.Amount : (decimal?)null,
                        Deduction = salaryComp.Amount.Value < 0 ? salaryComp.Amount.Value * -1 : (decimal?)null,
                        Earnings = salaryComp.Amount.Value > 0 ? salaryComp.Amount.Value * 1 : (decimal?)null,
                    });

                    vm.MasterSalaryComponents.SalaryStructure = new KeyValueViewModel()
                    {
                        Key = ssDto.SalaryStructureID.ToString(),
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
                }
            }

            foreach (var leavesalaryComp in ssDto.LeaveSalaryComponents)
            {
                if (leavesalaryComp != null)
                {
                    vm.MasterLeaveSalaryComponents.LeaveSalaryComponents.Add(new EmployeeLeaveSalaryStructureComponentViewModel()
                    {
                        EmployeeSalaryStructureComponentMapIID = leavesalaryComp.EmployeeSalaryStructureComponentMapIID,
                        EmployeeSalaryStructureID = leavesalaryComp.EmployeeSalaryStructureID,
                        SalaryComponentID = string.IsNullOrEmpty(leavesalaryComp.SalaryComponent.Key) ? (int?)null : int.Parse(leavesalaryComp.SalaryComponent.Key),
                        SalaryComponent = KeyValueViewModel.ToViewModel(leavesalaryComp.SalaryComponent),
                        //Amount = salaryComp.Amount.HasValue ? salaryComp.Amount : (decimal?)null,
                        Deduction = leavesalaryComp.Amount.Value < 0 ? leavesalaryComp.Amount.Value * -1 : (decimal?)null,
                        Earnings = leavesalaryComp.Amount.Value > 0 ? leavesalaryComp.Amount.Value * 1 : (decimal?)null,
                    });

                    vm.MasterLeaveSalaryComponents.SalaryStructure = new KeyValueViewModel()
                    {
                        Key = ssDto.LeaveSalaryStructureID.ToString(),
                        Value = ssDto.EmployeeLeaveSalaryStructure.Value
                    };
                }
            }
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EmployeeSalaryStructureViewModel, EmployeeSalaryStructureDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<EmployeeSalaryStructureComponentViewModel, EmployeeSalaryStructureComponentMapDTO>.CreateMap();
            Mapper<SalaryTimeSheetSettingViewModel, EmployeeSalaryStructureDTO>.CreateMap();
            var dto = Mapper<EmployeeSalaryStructureViewModel, EmployeeSalaryStructureDTO>.Map(this);
            dto.EmployeeSalaryStructureIID = this.EmployeeSalaryStructureIID;
            dto.IsActive = this.IsActive;
            //dto.FromDate = this.Fromdate;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.FromDate = string.IsNullOrEmpty(this.FromdateString) ? (DateTime?)null : DateTime.ParseExact(this.FromdateString, dateFormat, CultureInfo.InvariantCulture);
            
            dto.SalaryStructureID = string.IsNullOrEmpty(this.MasterSalaryComponents.SalaryStructure.Key) ? (long?)null : long.Parse(this.MasterSalaryComponents.SalaryStructure.Key);
            dto.LeaveSalaryStructureID = string.IsNullOrEmpty(this.MasterLeaveSalaryComponents.SalaryStructure.Key) ? (long?)null : long.Parse(this.MasterLeaveSalaryComponents.SalaryStructure.Key);
            dto.PayrollFrequencyID = string.IsNullOrEmpty(this.PayrollFrequency.Key) ? (byte?)null : byte.Parse(this.PayrollFrequency.Key);

            dto.PaymentModeID = string.IsNullOrEmpty(this.PaymentMode.Key) ? (byte?)null : byte.Parse(this.PaymentMode.Key);
            dto.AccountID = this.SalaryAccount == null || string.IsNullOrEmpty(this.SalaryAccount.Key) ? (long?)null : long.Parse(this.SalaryAccount.Key);
            dto.SalaryComponents = new List<EmployeeSalaryStructureComponentMapDTO>();
            dto.LeaveSalaryComponents = new List<EmployeeSalaryStructureComponentMapDTO>();

            List<KeyValueDTO> employeeList = new List<KeyValueDTO>();

            foreach (KeyValueViewModel em in this.Employee)
            {
                employeeList.Add(new KeyValueDTO { Key = em.Key, Value = em.Value }
                    );
            }
            dto.Employee = employeeList;

            foreach (var salaryComp in this.MasterSalaryComponents.SalaryComponents)
            {
                if (salaryComp.SalaryComponent != null && !string.IsNullOrEmpty(salaryComp.SalaryComponent.Key))
                {
                    dto.SalaryComponents.Add(new EmployeeSalaryStructureComponentMapDTO()
                    {
                        EmployeeSalaryStructureComponentMapIID = salaryComp.EmployeeSalaryStructureComponentMapIID,
                        EmployeeSalaryStructureID = salaryComp.EmployeeSalaryStructureID,
                        SalaryComponentID = string.IsNullOrEmpty(salaryComp.SalaryComponent.Key) ? (int?)null : int.Parse(salaryComp.SalaryComponent.Key),
                        Amount = salaryComp.Earnings.HasValue ? salaryComp.Earnings : salaryComp.Deduction.HasValue ? salaryComp.Deduction * -1 : (decimal?)null,

                    });
                }
            }

            foreach (var leaveSalaryComp in this.MasterLeaveSalaryComponents.LeaveSalaryComponents)
            {
                if (leaveSalaryComp.SalaryComponent != null && !string.IsNullOrEmpty(leaveSalaryComp.SalaryComponent.Key))
                {
                    dto.LeaveSalaryComponents.Add(new EmployeeSalaryStructureComponentMapDTO()
                    {
                        EmployeeSalaryStructureComponentMapIID = leaveSalaryComp.EmployeeSalaryStructureComponentMapIID,
                        EmployeeSalaryStructureID = leaveSalaryComp.EmployeeSalaryStructureID,
                        SalaryComponentID = string.IsNullOrEmpty(leaveSalaryComp.SalaryComponent.Key) ? (int?)null : int.Parse(leaveSalaryComp.SalaryComponent.Key),
                        Amount = leaveSalaryComp.Earnings.HasValue ? leaveSalaryComp.Earnings : leaveSalaryComp.Deduction.HasValue ? leaveSalaryComp.Deduction * -1 : (decimal?)null,

                    });
                }
            }

            dto.Amount = dto.SalaryComponents.Sum(x=>(x.Amount));
            dto.TimeSheetMaximumBenefits = this.TimeSheetSetting.TimeSheetMaximumBenefits;
            dto.IsSalaryBasedOnTimeSheet = this.TimeSheetSetting.IsSalaryBasedOnTimeSheet;
            dto.TimeSheetLeaveEncashmentPerDay = this.TimeSheetSetting.TimeSheetLeaveEncashmentPerDay;
            dto.TimeSheetHourRate = this.TimeSheetSetting.TimeSheetHourRate;
            dto.TimeSheetSalaryComponentID = this.TimeSheetSetting.TimeSheetSalaryComponent == null ||
            string.IsNullOrEmpty(this.TimeSheetSetting.TimeSheetSalaryComponent.Key) ? (int?)null : int.Parse(this.TimeSheetSetting.TimeSheetSalaryComponent.Key);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeSalaryStructureDTO>(jsonString);
        }
    }
}