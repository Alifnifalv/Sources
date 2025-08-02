using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Fees
{
    public class PaymentModeViewModel : BaseMasterViewModel
    {
        public PaymentModeViewModel()
        {
            Account = new KeyValueViewModel();
            TenderType = new KeyValueViewModel();
        }

        public int PaymentModeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("PaymentModeName")]
        public string PaymentModeName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Account")]
        [Select2("Account", "Numeric", false)]
        [LazyLoad("", "Mutual/GetLazyLookUpData?lookType=Account", "LookUps.Account")]
        public KeyValueViewModel Account { get; set; }
        public long? AccountId { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("TenderType")]
        [Select2("TenderType", "Numeric", false)]
        [LookUp("LookUps.TenderType")]
        public KeyValueViewModel TenderType { get; set; }
        public int? TenderTypeID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as PaymentModeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<PaymentModeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<PaymentModeDTO, PaymentModeViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var sDto = dto as PaymentModeDTO;
            var vm = Mapper<PaymentModeDTO, PaymentModeViewModel>.Map(dto as PaymentModeDTO);
            vm.Account = new KeyValueViewModel()
            {
                Key = sDto.Account.Key.ToString(),
                Value = sDto.Account.Value
            };
            vm.TenderType = new KeyValueViewModel()
            {
                Key = sDto.TenderType.Key.ToString(),
                Value = sDto.TenderType.Value
            };
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<PaymentModeViewModel, PaymentModeDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<PaymentModeViewModel, PaymentModeDTO>.Map(this);
            dto.AccountId = string.IsNullOrEmpty(this.Account.Key) ? (long?)null : long.Parse(this.Account.Key);
            dto.TenderTypeID = string.IsNullOrEmpty(this.TenderType.Key) ? (int?)null : int.Parse(this.TenderType.Key);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<PaymentModeDTO>(jsonString);
        }

    }
}