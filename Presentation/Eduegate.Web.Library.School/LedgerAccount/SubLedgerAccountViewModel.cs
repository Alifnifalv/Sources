using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.LedgerAccount;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.LedgerAccount
{
    public class SubLedgerAccountViewModel : BaseMasterViewModel
    {
        public SubLedgerAccountViewModel()
        {
            IsHidden = false;
            AllowUserDelete = false;
            AllowUserEdit = false;
            AllowUserRename = false;
            Accounts = new List<KeyValueViewModel>();
        }

        public long SL_AccountID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("SubLedgerAccountCode")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string SL_AccountCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("SubLedgerAccountName")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string SL_AccountName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("SubLedgerAlias")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string SL_Alias { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Stop", "Numeric", true, isFreeText: true)]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Accounts", "LookUps.Accounts")]
        [CustomDisplay("Accounts")]
        public List<KeyValueViewModel> Accounts { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsHidden")]
        public bool? IsHidden { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("AllowUserDelete")]
        public bool? AllowUserDelete { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("AllowUserEdit")]
        public bool? AllowUserEdit { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("AllowUserRename")]
        public bool? AllowUserRename { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SubLedgerAccountDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SubLedgerAccountViewModel>(jsonString);
        }
        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SubLedgerAccountDTO, SubLedgerAccountViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            //Mapper<SubLedgerAccountRelationDTO, SubLedgerAccountRelationViewModel>.CreateMap();
            var sublDto = dto as SubLedgerAccountDTO;
            var vm = Mapper<SubLedgerAccountDTO, SubLedgerAccountViewModel>.Map(sublDto);
            vm.SL_AccountID = sublDto.SL_AccountID;
            vm.SL_AccountCode = sublDto.SL_AccountCode;
            vm.SL_AccountName = sublDto.SL_AccountName;
            vm.SL_Alias = sublDto.SL_Alias;
            vm.CreatedBy = sublDto.CreatedBy;
            vm.CreatedDate = sublDto.CreatedDate;
            vm.UpdatedBy = sublDto.UpdatedBy;
            vm.UpdatedDate = sublDto.UpdatedDate;
            vm.IsHidden = sublDto.IsHidden;
            vm.AllowUserDelete = sublDto.AllowUserDelete;
            vm.AllowUserEdit = sublDto.AllowUserEdit;
            vm.AllowUserRename = sublDto.AllowUserRename;
         

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SubLedgerAccountViewModel, SubLedgerAccountDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            //Mapper<SubLedgerAccountRelationViewModel, SubLedgerAccountRelationDTO>.CreateMap();
            var dto = Mapper<SubLedgerAccountViewModel, SubLedgerAccountDTO>.Map(this);
            dto.SL_AccountID = this.SL_AccountID;
            dto.SL_AccountCode = this.SL_AccountCode;
            dto.SL_AccountName = this.SL_AccountName;
            dto.SL_Alias = this.SL_Alias;
            dto.CreatedBy = this.CreatedBy;
            dto.CreatedDate = this.CreatedDate;
            dto.UpdatedBy = this.UpdatedBy;
            dto.UpdatedDate = this.UpdatedDate;
            dto.IsHidden = this.IsHidden;
            dto.AllowUserDelete = this.AllowUserDelete;
            dto.AllowUserEdit = this.AllowUserEdit;
            dto.AllowUserRename = this.AllowUserRename;
           

            List<KeyValueDTO> AccountsList = new List<KeyValueDTO>();

            foreach (KeyValueViewModel vm in this.Accounts)
            {
                AccountsList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
            }
            dto.Accounts = AccountsList;

            return dto;
        }
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SubLedgerAccountDTO>(jsonString);
        }
    }
}