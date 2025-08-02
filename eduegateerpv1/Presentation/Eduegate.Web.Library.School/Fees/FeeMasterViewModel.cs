using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FeeMaster", "CRUDModel.ViewModel")]
    [DisplayName("Fee Master")]
    public class FeeMasterViewModel : BaseMasterViewModel
    {
        public FeeMasterViewModel()
        {
            AccountSetting = new FeeMasterAccountSettingViewModel();
        }
        public int FeeMasterID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("FeeType")]
        [LookUp("LookUps.FeeType")]
        public string FeeType { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(6, ErrorMessage = "Maximum Length should be within 6!")]
        [CustomDisplay("DefaultAmount")]
        public decimal? Amount { get; set; }

        public int? FeeTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Hidden)]
       // [DisplayName("Due Date")]
        public string DueDateString { get; set; }

        public System.DateTime? DueDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }  
        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("FeeCycle")]
        [LookUp("LookUps.FeeCycle")]
        public string FeeCycle { get; set; }

        public byte? FeeCycleID { get; set; }      
       
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [CustomDisplay("DueInDays")]
        public int? DueInDays { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsExternal")]
        public bool? IsExternal { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Reportname")]
        public string ReportName { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "AccountSetting", "AccountSetting")]
        [CustomDisplay("AccountSettings")]
        public FeeMasterAccountSettingViewModel AccountSetting { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeeMasterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeMasterViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FeeMasterDTO, FeeMasterViewModel>.CreateMap();
            Mapper<FeeMasterAccountSettingDTO,FeeMasterAccountSettingViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var feeDto = dto as FeeMasterDTO;
            var vm = Mapper<FeeMasterDTO, FeeMasterViewModel>.Map(feeDto);
            vm.FeeType = feeDto.FeeTypeID.ToString();
            vm.FeeCycle = feeDto.FeeCycleID.ToString();
            vm.AccountSetting = new FeeMasterAccountSettingViewModel();
            vm.AccountSetting.LedgerAccount = KeyValueViewModel.ToViewModel(feeDto.LedgerAccount);
            vm.AccountSetting.TaxLedgerAccount = KeyValueViewModel.ToViewModel(feeDto.TaxLedgerAccount);
            vm.AccountSetting.TaxPercentage = feeDto.TaxPercentage;
            vm.AccountSetting.OutstandingAccount = KeyValueViewModel.ToViewModel(feeDto.OutstandingAccount);
            vm.AccountSetting.OSTaxAccount = KeyValueViewModel.ToViewModel(feeDto.OSTaxAccount);
            vm.AccountSetting.OSTaxPercentage = feeDto.OSTaxPercentage;
            vm.AccountSetting.AdvanceAccount = KeyValueViewModel.ToViewModel(feeDto.AdvanceAccount);
            vm.AccountSetting.AdvanceTaxAccount = KeyValueViewModel.ToViewModel(feeDto.AdvanceTaxAccount);
            vm.AccountSetting.AdvanceAccount = KeyValueViewModel.ToViewModel(feeDto.AdvanceAccount);
            vm.AccountSetting.ProvisionforAdvanceAccount = KeyValueViewModel.ToViewModel(feeDto.ProvisionforAdvanceAccount);
            vm.AccountSetting.ProvisionforOutstandingAccount = KeyValueViewModel.ToViewModel(feeDto.ProvisionforOutstandingAccount);
            vm.AccountSetting.AdvanceTaxPercentage = feeDto.AdvanceTaxPercentage;
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeeMasterViewModel, FeeMasterDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<FeeMasterAccountSettingViewModel, FeeMasterAccountSettingDTO>.CreateMap();
            var dto = Mapper<FeeMasterViewModel, FeeMasterDTO>.Map(this);
            dto.FeeTypeID = Convert.ToInt32(this.FeeType);

            dto.LedgerAccountID = this.AccountSetting.LedgerAccount == null ||
             string.IsNullOrEmpty(this.AccountSetting.LedgerAccount.Key) ? (long?)null : long.Parse(this.AccountSetting.LedgerAccount.Key);

           
            dto.TaxLedgerAccountID = this.AccountSetting.TaxLedgerAccount == null ||
             string.IsNullOrEmpty(this.AccountSetting.TaxLedgerAccount.Key) ? (long?)null : long.Parse(this.AccountSetting.TaxLedgerAccount.Key);

            dto.FeeCycleID = string.IsNullOrEmpty(this.FeeCycle) ? (byte?)null : byte.Parse(this.FeeCycle);
            dto.DueDate = string.IsNullOrEmpty(this.DueDateString) ? (DateTime?)null : DateTime.Parse(DueDateString);
            dto.TaxPercentage = this.AccountSetting.TaxPercentage;

            dto.AdvanceAccountID = this.AccountSetting.AdvanceAccount == null ||
             string.IsNullOrEmpty(this.AccountSetting.AdvanceAccount.Key) ? (long?)null : long.Parse(this.AccountSetting.AdvanceAccount.Key);


            dto.AdvanceTaxAccountID = this.AccountSetting.AdvanceTaxAccount == null ||
             string.IsNullOrEmpty(this.AccountSetting.AdvanceTaxAccount.Key) ? (long?)null : long.Parse(this.AccountSetting.AdvanceTaxAccount.Key);
            dto.AdvanceTaxPercentage = this.AccountSetting.AdvanceTaxPercentage;

            dto.OSTaxAccountID = this.AccountSetting.OSTaxAccount == null ||
            string.IsNullOrEmpty(this.AccountSetting.OSTaxAccount.Key) ? (long?)null : long.Parse(this.AccountSetting.OSTaxAccount.Key);


            dto.OutstandingAccountID = this.AccountSetting.OutstandingAccount == null ||
             string.IsNullOrEmpty(this.AccountSetting.OutstandingAccount.Key) ? (long?)null : long.Parse(this.AccountSetting.OutstandingAccount.Key);
            dto.OSTaxPercentage = this.AccountSetting.OSTaxPercentage;

            dto.ProvisionforAdvanceAccountID = this.AccountSetting.ProvisionforAdvanceAccount == null ||
             string.IsNullOrEmpty(this.AccountSetting.ProvisionforAdvanceAccount.Key) ? (long?)null : long.Parse(this.AccountSetting.ProvisionforAdvanceAccount.Key);

            dto.ProvisionforOutstandingAccountID = this.AccountSetting.ProvisionforOutstandingAccount == null ||
            string.IsNullOrEmpty(this.AccountSetting.ProvisionforOutstandingAccount.Key) ? (long?)null : long.Parse(this.AccountSetting.ProvisionforOutstandingAccount.Key);
          
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeMasterDTO>(jsonString);
        }
    }
}

