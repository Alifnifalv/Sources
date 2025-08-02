using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Accounts;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;

namespace Eduegate.Web.Library.School.Accounts
{
    public class AdditionalExpenseViewModel : BaseMasterViewModel
    {
        public AdditionalExpenseViewModel()
        {
        }

        public int AdditionalExpenseID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Additional Expense Code")]
        [MaxLength(30, ErrorMessage = "Maximum Length should be within 30!")]
        public string AdditionalExpenseCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Additional Expense Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string AdditionalExpenseName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Account")]
        [LookUp("LookUps.AccountBehavior")]
        public string Account { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AdditionalExpensDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AdditionalExpenseViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AdditionalExpensDTO, AdditionalExpenseViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var agDto = dto as AdditionalExpensDTO;
            var vm = Mapper<AdditionalExpensDTO, AdditionalExpenseViewModel>.Map(dto as AdditionalExpensDTO);
            vm.AdditionalExpenseID = agDto.AdditionalExpenseID;
            vm.AdditionalExpenseCode = agDto.AdditionalExpenseCode;
            vm.AdditionalExpenseName = agDto.AdditionalExpenseName;
            vm.Account = agDto.AccountID.ToString();
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AdditionalExpenseViewModel, AdditionalExpensDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<AdditionalExpenseViewModel, AdditionalExpensDTO>.Map(this);
            dto.AdditionalExpenseID = this.AdditionalExpenseID;
            dto.AdditionalExpenseCode = this.AdditionalExpenseCode;
            dto.AdditionalExpenseName = this.AdditionalExpenseName;
            dto.AccountID = string.IsNullOrEmpty(this.Account) ? (byte?)null : byte.Parse(this.Account);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AdditionalExpensDTO>(jsonString);
        }
    }
}
