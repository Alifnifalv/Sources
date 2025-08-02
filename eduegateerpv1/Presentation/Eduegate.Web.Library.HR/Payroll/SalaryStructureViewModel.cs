using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SalaryStructure", "CRUDModel.ViewModel")]
    [DisplayName("Salary")]
    public class SalaryStructureViewModel : BaseMasterViewModel
    {
        public SalaryStructureViewModel()
        {
            SalaryComponents = new List<SalaryStructureComponentViewModel>() { new SalaryStructureComponentViewModel() };
            TimeSheetSetting = new SalaryTimeSheetSettingViewModel();
            IsActive = false;
        }

       
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("SalaryStructureID")]
        public long SalaryStructureID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(250,ErrorMessage = "Maximum Length should be within 250!")]
        [CustomDisplay("StructureName")]
        public string StructureName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("PayrollFrequency", "Numeric", false, "")]
        [LookUp("LookUps.PayrollFrequency")]
        [CustomDisplay("PayrollFrequency")]
        public KeyValueViewModel PayrollFrequency { get; set; }
        public byte? PayrollFrequencyID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("PaymentMode", "Numeric", false, "")]
        [LookUp("LookUps.PaymentModes")]
        [CustomDisplay("PaymentMode")]

        public KeyValueViewModel PaymentMode { get; set; }
        public int? PaymentModeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine10 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SalaryAccount", "Numeric", false, "")]
        [LookUp("LookUps.SalaryAccount")]
        [CustomDisplay("Account")]

        public KeyValueViewModel Account { get; set; }

        public long? AccountID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Components")]
        public List<SalaryStructureComponentViewModel> SalaryComponents { get; set; }
        public string OperatorString { get; set; }
        public string OtherComponentString { get; set; }
        public string VariablesString { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "TimeSheetSetting", "TimeSheetSetting")]
        [CustomDisplay("TimesheetSetting")]
        public SalaryTimeSheetSettingViewModel TimeSheetSetting { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SalaryStructureDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalaryStructureViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SalaryStructureDTO, SalaryStructureViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<SalaryStructureComponentDTO, SalaryStructureComponentViewModel>.CreateMap();
            Mapper<SalaryStructureDTO, SalaryTimeSheetSettingViewModel >.CreateMap();
            Mapper<SalaryStructureDTO, SalaryStructureComponentViewModel>.CreateMap();
            var ssDto = dto as SalaryStructureDTO;
            var vm = Mapper<SalaryStructureDTO, SalaryStructureViewModel>.Map(ssDto);
            vm.SalaryComponents = new List<SalaryStructureComponentViewModel>();
            foreach (var Component in ssDto.SalaryComponents)
            {
                vm.SalaryComponents.Add(new SalaryStructureComponentViewModel()
                {
                    SalaryStructureComponentMapIID = Component.SalaryStructureComponentMapIID,
                   
                    SalaryComponentID = string.IsNullOrEmpty(Component.SalaryComponent.Key) ? (int?)null : int.Parse(Component.SalaryComponent.Key),
                    SalaryComponent = KeyValueViewModel.ToViewModel(Component.SalaryComponent),
                    MinAmount = Component.MinAmount.HasValue ? Component.MinAmount : (decimal?)null,
                    MaxAmount = Component.MaxAmount.HasValue ? Component.MaxAmount : (decimal?)null

                });
                vm.TimeSheetSetting.TimeSheetMaximumBenefits = ssDto.TimeSheetMaximumBenefits;
                vm.TimeSheetSetting.IsSalaryBasedOnTimeSheet = ssDto.IsSalaryBasedOnTimeSheet;
                vm.TimeSheetSetting.TimeSheetLeaveEncashmentPerDay = ssDto.TimeSheetLeaveEncashmentPerDay;
                vm.TimeSheetSetting.TimeSheetHourRate = ssDto.TimeSheetHourRate;
                vm.TimeSheetSetting.TimeSheetSalaryComponentID = ssDto.TimeSheetSalaryComponentID;
                vm.TimeSheetSetting.TimeSheetSalaryComponent = ssDto.TimeSheetSalaryComponentID.HasValue ? new KeyValueViewModel() { Key = ssDto.TimeSheetSalaryComponentID.ToString(), Value = ssDto.TimeSheetSalaryComponent.Value } : null;
            }
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SalaryStructureViewModel, SalaryStructureDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<SalaryStructureComponentViewModel, SalaryStructureComponentDTO>.CreateMap();
            Mapper<SalaryTimeSheetSettingViewModel, SalaryStructureDTO>.CreateMap();
            var dto = Mapper<SalaryStructureViewModel, SalaryStructureDTO>.Map(this);
            dto.StructureName = this.StructureName;
            dto.PayrollFrequencyID = string.IsNullOrEmpty(this.PayrollFrequency.Key) ? (byte?)null : byte.Parse(this.PayrollFrequency.Key);
            dto.PaymentModeID = string.IsNullOrEmpty(this.PaymentMode.Key) ? (byte?)null : byte.Parse(this.PaymentMode.Key);
            dto.AccountID = this.Account == null || string.IsNullOrEmpty(this.Account.Key) ? (byte?)null : byte.Parse(this.Account.Key);
            dto.SalaryComponents = new List<SalaryStructureComponentDTO>();

            foreach (var salaryComp in this.SalaryComponents)
            {
                if (!string.IsNullOrEmpty(salaryComp.SalaryComponent.Key))
                {
                    dto.SalaryComponents.Add(new SalaryStructureComponentDTO()
                    {
                        SalaryStructureComponentMapIID = salaryComp.SalaryStructureComponentMapIID,
                        //ComponentTypeID=salaryComp.ComponentTypeID,
                        SalaryComponentID = string.IsNullOrEmpty(salaryComp.SalaryComponent.Key) ? (int?)null : int.Parse(salaryComp.SalaryComponent.Key),
                        MinAmount = salaryComp.MinAmount.HasValue ? salaryComp.MinAmount : (decimal?)null,
                        MaxAmount = salaryComp.MaxAmount.HasValue ? salaryComp.MaxAmount : (decimal?)null
                    });
                }
            
            dto.TimeSheetMaximumBenefits = this.TimeSheetSetting.TimeSheetMaximumBenefits;
            dto.IsSalaryBasedOnTimeSheet = this.TimeSheetSetting.IsSalaryBasedOnTimeSheet;
            dto.TimeSheetLeaveEncashmentPerDay = this.TimeSheetSetting.TimeSheetLeaveEncashmentPerDay;
            dto.TimeSheetHourRate = this.TimeSheetSetting.TimeSheetHourRate;
            dto.TimeSheetSalaryComponentID = this.TimeSheetSetting.TimeSheetSalaryComponent == null ||
             string.IsNullOrEmpty(this.TimeSheetSetting.TimeSheetSalaryComponent.Key)? (int?)null : int.Parse(this.TimeSheetSetting.TimeSheetSalaryComponent.Key);
            }
            return dto;
           
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalaryStructureDTO>(jsonString);
        }
    }
}
